using System;
using System.Collections.Generic;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using System.IO;
using SQLite;

namespace Foosball_Android
{
    class ScoreListFragment : Fragment, ScoreListAdapter.OnCardClickListener
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
            recyclerView.SetAdapter(new ScoreListAdapter(getFinalResult(), this));
            recyclerView.Invalidate();
            return view;
        }

      
        public List<ScoreModel> getFinalResult()
        {
            try
            {
                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "scoresDB.db3"); //Call Database  
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

        public void OnItemClick(int redTeam, int blueTeam, long id)
        {
            Bundle bundle = new Bundle();
            bundle.PutInt("redTeam", redTeam);
            bundle.PutInt("blueTeam", blueTeam);
            bundle.PutLong("id", id);
            EditScoreFragment editScoreFragment = new EditScoreFragment();
            editScoreFragment.Arguments = bundle;
            FragmentTransaction fragmentTransaction = FragmentManager.BeginTransaction();
            fragmentTransaction.Replace(Resource.Id.main_frame_layout, editScoreFragment).AddToBackStack(null);
            fragmentTransaction.Commit();
        }
    }
}