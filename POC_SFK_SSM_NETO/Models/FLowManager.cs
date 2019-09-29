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

namespace POC_SFK_SSM_NETO.Models
{
    public class FLowManager
    {
        public FLowManager(Flow flow, Dictionary<string, object> startingData)
        {
            this.Flow = flow;
            this.ManagedValues = new Dictionary<string, object>(startingData);
        }

        private Dictionary<string, object> ManagedValues = new Dictionary<string, object>();

        private List<Dictionary<string, object>> InputSnapshots = new List<Dictionary<string, object>>();

        private Flow Flow { get; set; }

        public Type GetNextScreen()
        {
            var nextScreenType = this.Flow.GetNextScreen();

            if (nextScreenType != null)
            {
                this.InputSnapshots.Add(new Dictionary<string, object>(this.ManagedValues));
            }

            return nextScreenType;
        }

        public Type GetPriorScreen()
        {
            var priorScreenType = this.Flow.GetPriorScreen();

            if (priorScreenType != null)
            {
                this.InputSnapshots.RemoveAt(this.InputSnapshots.Count - 1);
                this.ManagedValues = new Dictionary<string, object>(this.InputSnapshots.Last());
            }

            return priorScreenType;
        }

        public IEnumerable<string> GetRequiredInputKeys(Type screenType)
        {
            IFlowInputOutput screenInstance = (IFlowInputOutput)screenType.GetConstructor(new Type[0]).Invoke(new object[0]);

            return screenInstance.NeededInput();
        }

        public Dictionary<string, object> GetInput(Type screenType)
        {
            var requiredInputKeys = this.GetRequiredInputKeys(screenType);

            return this.ManagedValues
                    .Where(item => requiredInputKeys.Contains(item.Key))
                    .ToDictionary(x => x.Key, y => y.Value);
        }

        public void SaveResultValues(Dictionary<string, object> resultValues)
        {
            foreach (var resultValue in resultValues)
            {
                this.ManagedValues[resultValue.Key] = resultValue.Value;
            }
        }
    }
}