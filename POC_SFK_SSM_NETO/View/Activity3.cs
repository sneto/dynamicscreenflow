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
using Newtonsoft.Json;
using POC_SFK_SSM_NETO.View.Flow;

namespace POC_SFK_SSM_NETO.View
{
    [Activity(Label = "Activity3")]
    public class Activity3 : FlowManagerBaseActivity
    {
        private string CustomerCode { get; set; }

        public override IEnumerable<string> NeededInput()
        {
            return new List<string>(new[] {
                ActivitiesFlowManager.IntentCustomerCode,
                ActivitiesFlowManager.IntentOrderCode,
            });
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            this.SetContentView(Resource.Layout.Activity3);

            this.CustomerCode = this.Intent.GetStringExtra(ActivitiesFlowManager.IntentCustomerCode);
            var orderCode = this.Intent.GetStringExtra(ActivitiesFlowManager.IntentOrderCode);

            this.FindViewById<TextView>(Resource.Id.activity3_customerCode).Text = this.CustomerCode;
            this.FindViewById<TextView>(Resource.Id.activity3_orderCode).Text = orderCode;

            this.FindViewById<Button>(Resource.Id.activity3_startSecondFlow).Click += StartSecondFlowClick;
        }

        private void StartSecondFlowClick(object sender, EventArgs e)
        {
            var flowManagerIntent = new Intent(this, typeof(ActivitiesFlowManager));
            flowManagerIntent.PutExtra(ActivitiesFlowManager.IntentInputFlowId, 2);

            var flow2StartingData = new Dictionary<string, object>
            {
                { ActivitiesFlowManager.IntentCustomerCode, this.CustomerCode}
            };

            flowManagerIntent.PutExtra(ActivitiesFlowManager.IntentInputFlowStartingData, JsonConvert.SerializeObject(flow2StartingData));
            StartActivity(flowManagerIntent);
        }
    }
}