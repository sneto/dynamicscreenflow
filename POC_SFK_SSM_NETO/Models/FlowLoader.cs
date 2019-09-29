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
    public class FlowLoader
    {
        public static Flow LoadFlow(int id, ScreensMapping screensMapping)
        {
            IEnumerable<int> screenIds;
            // TODO Ler do banco de dados o invés dos valores fixos
            if (id == 1)
            {
                screenIds = GetSequence1();
            }
            else
            {
                screenIds = GetSequence2();
            }

            var flow = new Flow(id);

            foreach (var screenId in screenIds)
            {
                // TODO validar se o ID é válido
                flow.AddScreen(screensMapping.Mapping[screenId]);
            }

            return flow;
        }

        private static IEnumerable<int> GetSequence1()
        {
            return new List<int> { 1, 2, 3 };
        }

        private static IEnumerable<int> GetSequence2()
        {
            return new List<int> { 4, 5, 1 };
        }
    }
}