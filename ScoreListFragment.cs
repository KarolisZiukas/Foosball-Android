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
using Android.Support.V7.Widget;
using System.IO;
using SQLite;

namespace Foosball_Android
{
    class ScoreListFragment : Fragment
    {
        RecyclerView recyclerView;
        List<ScoreModel> results;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            getFinalResult();
            View view = inflater.Inflate(Resource.Layout.ScoresList, null);
            recyclerView = view.FindViewById<RecyclerView>(Resource.Id.scores_list_fragment);
            recyclerView.SetLayoutManager(new LinearLayoutManager(this.Activity));
            recyclerView.HasFixedSize = true;
            recyclerView.SetAdapter(new ScoreListAdapter(getFinalResult()));
            return view;
        }


        public List<ScoreModel> getFinalResult()
        {
            try
            {
                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "scores.db3"); //Call Database  
                var db = new SQLiteConnection(dpPath);
                var data = db.Table<ScoreModel>(); //Call Table
                results = db.Query<ScoreModel>("SELECT * from [ScoreModel]");
                return results;
            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, ex.ToString(), ToastLength.Short).Show();
                return null;
            }
        }


    }
}