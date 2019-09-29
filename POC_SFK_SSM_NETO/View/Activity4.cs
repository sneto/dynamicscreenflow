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
    [Activity(Label = "Activity4")]
    public class Activity4 : FlowManagerBaseActivity
    {
        public override IEnumerable<string> NeededInput()
        {
            return new List<string>(new[] {
                ActivitiesFlowManager.IntentCustomerCode
            });
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            this.SetContentView(Resource.Layout.Activity4);

            var customerCode = this.Intent.GetStringExtra(ActivitiesFlowManager.IntentCustomerCode);

            this.FindViewById<TextView>(Resource.Id.activity4_customerCode).Text = customerCode;

            this.SetResultValue(ActivitiesFlowManager.IntentCustomerCode, "Input de cliente vindo da tela 4");
            this.SetResultValue(ActivitiesFlowManager.IntentOrderCode, "Input de pedido vindo da tela 4");
        }
    }
}