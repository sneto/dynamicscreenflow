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
    [Activity(Label = "Activity1")]
    public class Activity1 : FlowManagerBaseActivity
    {
        public override IEnumerable<string> NeededInput()
        {
            return new List<string>();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            this.SetContentView(Resource.Layout.Activity1);

            this.SetResultValue(ActivitiesFlowManager.IntentCustomerCode, "Input de cliente vindo da tela 1");
            this.SetResultValue(ActivitiesFlowManager.IntentOrderCode, "Input de pedido vindo da tela 1");
        }
    }
}