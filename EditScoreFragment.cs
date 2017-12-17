using System;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.IO;
using SQLite;

namespace Foosball_Android
{
    class EditScoreFragment : Fragment
    {
        EditText editRedTeamScore;
        EditText editBlueTeamScore;
        Button confirmEditBt;
        Button cancelEditBt;
        Button deleteScoreBt;
        long id;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
      
            View view = inflater.Inflate(Resource.Layout.Edit_score_fragment, null);
            editBlueTeamScore = view.FindViewById<EditText>(Resource.Id.blue_team_et);
            editRedTeamScore = view.FindViewById<EditText>(Resource.Id.red_team_et);
            confirmEditBt = view.FindViewById<Button>(Resource.Id.ok_score_edit_bt);
            cancelEditBt = view.FindViewById<Button>(Resource.Id.cancel_score_edit_bt);
            deleteScoreBt = view.FindViewById<Button>(Resource.Id.delete_score_edit_bt);
            Bundle bundle = this.Arguments;
            if (bundle != null)
            {
                editBlueTeamScore.Text = "" + bundle.GetInt("blueTeam");
                editRedTeamScore.Text = "" + bundle.GetInt("redTeam");
                id = bundle.GetLong("id");
            }
            confirmEditBt.Click += delegate
            {
                updateScore();
            };
            cancelEditBt.Click += delegate
            {
                cancelUpdate();
            };
            deleteScoreBt.Click += delegate
            {
                deleteScore();
            };

          


            return view;
        }

        private void deleteScore()
        {
            try
            {
                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "scoresDB.db3"); //Call Database  
                var db = new SQLiteConnection(dpPath);
                var data = db.Table<ScoreModel>(); 
                db.Query<ScoreModel>(String.Format("DELETE FROM [ScoreModel] WHERE [id] = {0}", id));

                FragmentManager fragmentManager = Activity.FragmentManager;
                fragmentManager.PopBackStack();
            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, ex.ToString(), ToastLength.Short).Show();
            }
        }

        private void updateScore()
        {
            try
            {
                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "scoresDB.db3"); //Call Database  
                var db = new SQLiteConnection(dpPath);
                var data = db.Table<ScoreModel>(); //Call Table
                db.Query<ScoreModel>(String.Format("UPDATE [ScoreModel] SET [redTeamScore] = {0}, [blueTeamScore] = {1} WHERE [id] = {2}", editRedTeamScore.Text, editBlueTeamScore.Text, id));
                FragmentManager fragmentManager = Activity.FragmentManager;
                fragmentManager.PopBackStack();
            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, ex.ToString(), ToastLength.Short).Show();
            }
           


        }

        private void cancelUpdate()
        {

            FragmentManager fragmentManager = Activity.FragmentManager;
            fragmentManager.PopBackStack();
         
        }
    }
}