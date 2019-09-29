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
    public class ScreensMapping
    {
        public Dictionary<int, Type> Mapping { get; set; } = new Dictionary<int, Type>();

        public void Add(int id, Type screenType)
        {
            this.Mapping.Add(id, screenType);
        }
    }
}