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

namespace Foosball_Android
{
    class Utility
    {
        public int CalCulateTotalScore(IEnumerable<TotalScore> lstHomeAppliance)
        {
            var total = 0;
            foreach (var p in lstHomeAppliance)
            {
                total += p.Score;
            }
            return total;
        }
    }
}