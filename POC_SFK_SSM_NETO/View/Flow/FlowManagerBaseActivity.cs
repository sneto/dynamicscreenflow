using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using POC_SFK_SSM_NETO.Models;

namespace POC_SFK_SSM_NETO.View.Flow
{
    public abstract class FlowManagerBaseActivity : Activity, IFlowInputOutput
    {
        public abstract IEnumerable<string> NeededInput();

        private Dictionary<string, object> ResultValues { get; } = new Dictionary<string, object>();

        /// <summary>
        /// content view override to inflate the client layout in layout flow controller
        /// </summary>
        /// <param name="layoutResID">Child layout that will be inflate</param>
        public override void SetContentView(int layoutResID)
        {
            base.SetContentView(Resource.Layout.LA_AC_Navigation);

            LayoutInflater inflater = LayoutInflater.From(this);
            Android.Views.View inflatedLayout = inflater.Inflate(layoutResID, null);
            LinearLayout ll = this.Window.FindViewById<LinearLayout>(Resource.Id.navigation_container);
            ll.AddView(inflatedLayout);
            this.SetNavigationEvents();
        }

        protected void SetResultValue(string resultKey, object resultValue)
        {
            this.ResultValues[resultKey] = resultValue;
        }

        private void SetNavigationEvents()
        {
            this.FindViewById<ImageButton>(Resource.Id.ac_navigation_ibt_previous).Click += NavPrevious_Click;
            this.FindViewById<ImageButton>(Resource.Id.ac_navigation_ibt_pause).Click += NavPause_Click;
            this.FindViewById<ImageButton>(Resource.Id.ac_navigation_ibt_forward).Click += NavForward_Click;
        }

        private void NavPause_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void NavForward_Click(object sender, EventArgs e)
        {
            this.FinishWithResult(ActivitiesFlowManager.FlowDirection.Forward);
        }

        private void NavPrevious_Click(object sender, EventArgs e)
        {
            this.FinishWithResult(ActivitiesFlowManager.FlowDirection.Backward);
        }

        private void FinishWithResult(ActivitiesFlowManager.FlowDirection direction)
        {
            var resultIntent = new Intent();
            resultIntent.PutExtra(ActivitiesFlowManager.IntentOutputDirection, (int)direction);

            foreach (var item in this.ResultValues)
            {
                if (item.Value is string)
                {
                    resultIntent.PutExtra(item.Key, (string)item.Value);
                }
                else if (item.Value is double)
                {
                    resultIntent.PutExtra(item.Key, (double)item.Value);
                }
                else if (item.Value is bool)
                {
                    resultIntent.PutExtra(item.Key, (bool)item.Value);
                }
                else if (item.Value is int)
                {
                    resultIntent.PutExtra(item.Key, (int)item.Value);
                }
            }

            this.SetResult(Result.Ok, resultIntent);
            this.Finish();
        }

        public override void OnBackPressed()
        {
            this.FinishWithResult(ActivitiesFlowManager.FlowDirection.Backward);
        }
    }
}