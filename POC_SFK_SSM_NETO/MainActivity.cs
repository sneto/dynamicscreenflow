using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using POC_SFK_SSM_NETO.View.Flow;
using Android.Content;

namespace POC_SFK_SSM_NETO
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            this.FindViewById<Button>(Resource.Id.activity_main_start_flow).Click += startflow_Click;
        }

        private void startflow_Click(object sender, System.EventArgs e)
        {
            var flowManagerIntent = new Intent(this, typeof(ActivitiesFlowManager));
            flowManagerIntent.PutExtra(ActivitiesFlowManager.IntentInputFlowId, 1);
            StartActivity(flowManagerIntent);
        }
    }
}