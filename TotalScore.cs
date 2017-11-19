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
    public abstract class TotalScore
    {
        public int Score { get; set; }
    }

    public abstract class RedTeamScore : TotalScore
    {

    }

    public abstract class BlueTeamScore : TotalScore
    {

    }
}