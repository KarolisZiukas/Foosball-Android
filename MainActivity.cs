using Android.App;
using Android.OS;
using System.Collections.Generic;
using SQLite;
using SQLitePCL;
using System;
using System.IO;
using Android.Views;
using Android.Widget;

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

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
 
            return base.OnOptionsItemSelected(item);
        }

        private void loadFragment()
        {
           
            FragmentTransaction fragmentTransaction = FragmentManager.BeginTransaction();
            fragmentTransaction.Replace(Resource.Id.main_frame_layout, new ScoreFragment<string>());
            fragmentTransaction.Commit();

        }
    }
}

