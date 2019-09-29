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
using POC_SFK_SSM_NETO.Models;

namespace POC_SFK_SSM_NETO.View.Flow
{
    [Activity(Label = "ActivitiesFlowManager")]
    public class ActivitiesFlowManager : Activity
    {
        public static readonly string IntentInputFlowStartingData = "ActivitiesFlowManagerInputFlowStartingData";

        public static readonly string IntentInputFlowId = "ActivitiesFlowManagerFlowId";

        public static readonly string IntentOutputDirection = "ActivitiesFlowManagerDirection";

        public static readonly string IntentCustomerCode = "ActivitiesFlowManagerCustomerCode";

        public static readonly string IntentOrderCode = "ActivitiesFlowManagerOrderCode";

        public enum FlowDirection
        {
            Forward,
            Backward
        }

        private FLowManager FlowManager { get; set; }

        private int FlowId { get; set; }

        private Dictionary<string, object> FlowStartingData { get; set; } = new Dictionary<string, object>();

        private ScreensMapping ActivitiesMapping { get; } = new ScreensMapping();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            this.InitializeActivity(savedInstanceState);
            this.ShowActivity(FlowDirection.Forward);
        }

        private void InitializeActivity(Bundle savedInstanceState)
        {
            this.LoadActivitiesMapping();

            // TODO Tratar código inválido, input faltando, etc
            this.FlowId = this.Intent.GetIntExtra(IntentInputFlowId, 0);
            var flowStartingData = this.Intent.GetStringExtra(IntentInputFlowStartingData);

            if (!string.IsNullOrEmpty(flowStartingData))
            {
                this.FlowStartingData = JsonConvert.DeserializeObject<Dictionary<string, object>>(flowStartingData);
            }

            this.LoadFlowManager();
        }

        private void LoadActivitiesMapping()
        {
            // TODO Ver onde poderia ficar esse código
            this.ActivitiesMapping.Add(1, typeof(Activity1));
            this.ActivitiesMapping.Add(2, typeof(Activity2));
            this.ActivitiesMapping.Add(3, typeof(Activity3));
            this.ActivitiesMapping.Add(4, typeof(Activity4));
            this.ActivitiesMapping.Add(5, typeof(Activity5));
        }

        private void LoadFlowManager()
        {
            var flow = FlowLoader.LoadFlow(this.FlowId, this.ActivitiesMapping);
            this.FlowManager = new FLowManager(flow, this.FlowStartingData);
        }

        private void ShowActivity(FlowDirection direction)
        {
            Type targetActivity;

            if (direction == FlowDirection.Forward)
            {
                targetActivity = this.FlowManager.GetNextScreen();
            }
            else
            {
                targetActivity = this.FlowManager.GetPriorScreen();
            }

            if (targetActivity == null)
            {
                this.Finish();
                return;
            }

            Intent nextActivityIntent = new Intent(this, targetActivity);

            var inputValues = this.GetScreenInput(targetActivity);

            this.SetInputValues(inputValues, nextActivityIntent);

            StartActivityForResult(nextActivityIntent, 0);
        }

        private void SetInputValues(Dictionary<string, object> inputValues, Intent intent)
        {
            if (inputValues != null)
            {
                foreach (var inputValue in inputValues)
                {
                    // TODO think about another option because it violates SOLID and 
                    // doesn't support all build in data types (decimal, for example)
                    if (inputValue.Value is int)
                    {
                        intent.PutExtra(inputValue.Key, (int)inputValue.Value);
                    }
                    else if (inputValue.Value is double)
                    {
                        intent.PutExtra(inputValue.Key, (double)inputValue.Value);
                    }
                    else if (inputValue.Value is string)
                    {
                        intent.PutExtra(inputValue.Key, (string)inputValue.Value);
                    }
                    else if (inputValue.Value is bool)
                    {
                        intent.PutExtra(inputValue.Key, (bool)inputValue.Value);
                    }
                    else
                    {
                        // Unknown type
                        // TODO Do something
                    }
                }
            }
        }

        private Dictionary<string, object> GetScreenInput(Type screenType)
        {
            return this.FlowManager.GetInput(screenType);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            var resultValues = this.GetResultValues(data);

            this.FlowManager.SaveResultValues(resultValues);

            var direction = (FlowDirection)resultValues[IntentOutputDirection];

            this.ShowActivity(direction);
        }

        private Dictionary<string, object> GetResultValues(Intent resultIntent)
        {
            var resultValues = new Dictionary<string, object>();

            foreach (var resultKey in resultIntent.Extras.KeySet())
            {
                var value = resultIntent.Extras.Get(resultKey);

                if (value is Java.Lang.Integer)
                {
                    resultValues.Add(resultKey, ((Java.Lang.Integer)value).IntValue());
                }
                else if (value is Java.Lang.Double)
                {
                    resultValues.Add(resultKey, ((Java.Lang.Double)value).DoubleValue());
                }
                else if (value is Java.Lang.String)
                {
                    resultValues.Add(resultKey, ((Java.Lang.String)value).ToString());
                }
                else if (value is Java.Lang.Boolean)
                {
                    resultValues.Add(resultKey, ((Java.Lang.Boolean)value).BooleanValue());
                }
            }

            return resultValues;
        }
    }
}