using Android.App;
using Android.OS;
using System.Collections.Generic;
using SQLite;
using SQLitePCL;
using System;
using System.IO;

namespace Foosball_Android
{
    [Activity(Label = "Foosball_Android", MainLauncher = true)]
    public class MainActivity : Activity
    {
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
            loadFragment();


        }

  

        private void loadFragment()
        {
           
            FragmentTransaction fragmentTransaction = FragmentManager.BeginTransaction();
            fragmentTransaction.Replace(Resource.Id.main_frame_layout, new ScoreFragment<string>());
            fragmentTransaction.Commit();

        }
    }
}

