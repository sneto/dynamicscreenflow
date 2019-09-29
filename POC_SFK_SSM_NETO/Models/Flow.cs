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
    public class Flow
    {
        public Flow(int id)
        {
            this.Id = id;
        }

        public int Id { get; set; }

        public int CurrentIndex { get; private set; } = -1;

        private List<Type> ScreenSequence { get; } = new List<Type>();

        public Type GetNextScreen()
        {
            if (this.CurrentIndex + 1 >= this.ScreenSequence.Count)
            {
                return null;
            }

            this.CurrentIndex++;
            return this.ScreenSequence[this.CurrentIndex];
        }

        public Type GetPriorScreen()
        {
            if (this.CurrentIndex - 1 < 0)
            {
                return null;
            }

            this.CurrentIndex--;
            return this.ScreenSequence[this.CurrentIndex];
        }

        public bool AddScreen(Type screenToAdd)
        {
            // TODO implementar validação?
            this.ScreenSequence.Add(screenToAdd);
            return true;
        }
    }
}