using System;
using System.Collections.Generic;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using System.IO;
using SQLite;
using System.Linq;

namespace Foosball_Android
{
    class ScoreListFragment : Fragment, ScoreListAdapter.OnCardClickListener
    {
        RecyclerView recyclerView;
        List<ScoreModel> results;
        public int whatToShow;
        
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
      
            getFinalResult();
            View view = inflater.Inflate(Resource.Layout.ScoresList, null);
            recyclerView = view.FindViewById<RecyclerView>(Resource.Id.scores_list_fragment);
            recyclerView.SetLayoutManager(new LinearLayoutManager(this.Activity));
            recyclerView.HasFixedSize = true;
            switch (whatToShow) {
                case 0:
                    recyclerView.SetAdapter(new ScoreListAdapter(getFinalResult(), this));
                    showQuickStats();

                    break;
                case 1:
                    recyclerView.SetAdapter(new ScoreListAdapter(getFinalxResult().ToList(), this));
                    break;
                case 2:
                    recyclerView.SetAdapter(new ScoreListAdapter(getSkippedxResult().ToList(), this));
                    break;
            }

            recyclerView.Invalidate();
            return view;
        }


        public ScoreListFragment(int whatToShow)
        {
            this.whatToShow = whatToShow;
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


        public IEnumerable<ScoreModel> getFinalxResult()
        {
            try
            {
        
                var rez = getFinalResult().Take(10);
                return rez;
            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, ex.ToString(), ToastLength.Short).Show();
                return null;
            }
        }

        public IEnumerable<ScoreModel> getSkippedxResult()
        {
            try
            {
                var rez = getFinalResult().Skip(10);

                return rez;
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

        public void showQuickStats()
        {
            int redTeamWon = results.Where(i => i.redTeamScore > i.blueTeamScore).Count();
            int blueTeamWon = results.Where(i => i.redTeamScore < i.blueTeamScore).Count();
            int ties = results.Where(i => i.redTeamScore == i.blueTeamScore).Count();
            double redTeamAvg = System.Math.Round(results.Average(s => s.redTeamScore), 2);
            double blueTeamAvg = System.Math.Round(results.Average(s => s.blueTeamScore), 2);
            AlertDialog.Builder alertDialog = new AlertDialog.Builder(Activity);
            alertDialog.SetTitle("Quick Stats");
            alertDialog.SetMessage("Blue team won: " + blueTeamWon + "\nRed Team won: " + redTeamWon + "\nTies: " + ties + "\nAvg Red Team score: " + redTeamAvg + "\nAvg Blue Team score: " + blueTeamAvg);
            alertDialog.SetNeutralButton("OK", delegate
            {
                alertDialog.Dispose();
            });
            alertDialog.Show();
        }
    }
}