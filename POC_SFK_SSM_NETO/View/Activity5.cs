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
using POC_SFK_SSM_NETO.View.Flow;

namespace POC_SFK_SSM_NETO.View
{
    [Activity(Label = "Activity5")]
    public class Activity5 : FlowManagerBaseActivity
    {
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

            this.SetContentView(Resource.Layout.Activity5);

            var customerCode = this.Intent.GetStringExtra(ActivitiesFlowManager.IntentCustomerCode);
            var orderCode = this.Intent.GetStringExtra(ActivitiesFlowManager.IntentOrderCode);

            this.FindViewById<TextView>(Resource.Id.activity5_customerCode).Text = customerCode;
            this.FindViewById<TextView>(Resource.Id.activity5_orderCode).Text = orderCode;
        }
    }
}