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
    public class FoulGoal : BlueTeamScore 
    {
        public FoulGoal()
        {
            Score = 1;
        }
    }
    public class SimpleGoal : BlueTeamScore
    {
        public SimpleGoal()
        {
            Score = 1;
        }
    }

    public class  AccidentalGoal: RedTeamScore
    {
        public AccidentalGoal()
        {
            Score = 1;
        }
    }

    public class PenaltyGoal : RedTeamScore
    {
        public PenaltyGoal()
        {
            Score = 1;
        }
    }

}