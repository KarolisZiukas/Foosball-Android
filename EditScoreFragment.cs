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

        public static string UnixTimeStampToDateTime(long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            //System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            //dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            //return dtDateTime;
            // This is an example of a UNIX timestamp for the date/time 11-04-2005 09:25.
            double timestamp = 1113211532;

            // First make a System.DateTime equivalent to the UNIX Epoch.
            System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);

            // Add the number of seconds in UNIX timestamp to be converted.
            dateTime = dateTime.AddSeconds(timestamp);

            // The dateTime now contains the right date/time so to format the string,
            // use the standard formatting methods of the DateTime object.
            string printDate;
            return printDate = dateTime.ToShortDateString() + " " + dateTime.ToShortTimeString();

            // Print the date and time
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