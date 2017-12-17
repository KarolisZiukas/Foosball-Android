
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Json;
using Newtonsoft.Json;
using System.Net;
using System;
using System.Threading.Tasks;
using Android.Media;
using SQLite;
using System.IO;

namespace Foosball_Android 
{


    [Activity(Label = "ScoreFragment")]
    public class ScoreFragment<T> : Fragment
    {
        MediaPlayer _mediaPlayer;
        TextView redTeamTextView;
        TextView blueTeamTextView;
        Button openDataTableBt;
        //Button openDataBaseBt;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            View view = inflater.Inflate(Resource.Layout.ScoreFragment, null);
            SetHasOptionsMenu(true);
            redTeamTextView = view.FindViewById<TextView>(Resource.Id.red_team_text_view);
            blueTeamTextView = view.FindViewById<TextView>(Resource.Id.blue_team_text_view);
            openDataTableBt = view.FindViewById<Button>(Resource.Id.open_dataTable);
            CreateDB();
            //AutoUpdate autoUpdate = new AutoUpdate();
            // autoUpdate.setTimer();
            
           // autoUpdate.showWhatYouGot();
            //ToDo Karolis: await/async
            redTeamTextView.Click += async (sender, e) =>
            {

                //autoUpdate.UpdateEventAsync(sender, e);
                //autoUpdate.showWhatYouGot();


                 insertEndResult();
                //    //string url = "http://172.24.2.174:5000/api/scores";
                //    //JsonValue json = await Fetchdata(url);

            };
            openDataTableBt.Click += delegate
            {
                loadFragment(0);
            };


            blueTeamTextView.Click += delegate
            {
                //_mediaPlayer = MediaPlayer.Create(Application.Context, Resource.Raw.blue_team_scored);
                //_mediaPlayer.Start();
            };

            return view;
        }

        private void loadFragment(int whatToShow)
        {
            FragmentTransaction fragmentTransaction = FragmentManager.BeginTransaction();
            fragmentTransaction.Replace(Resource.Id.main_frame_layout, new ScoreListFragment(whatToShow)).AddToBackStack(null);
            fragmentTransaction.Commit();

        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menu_preferences :
                    loadFragment(1);
                    break;
                case Resource.Id.menu_preferences2:
                    loadFragment(2);
                    break;
            }
               return base.OnOptionsItemSelected(item);
        }





        private async Task<JsonValue> Fetchdata(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";
            
            using (WebResponse response = await request.GetResponseAsync())
            {

                using (System.IO.Stream stream = response.GetResponseStream())

                {


                    ScoreModel scoreModel = new ScoreModel();
                    
                    JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));

                    foreach (var jjjson in jsonDoc)
                    {
                      var result = JsonConvert.DeserializeObject<ScoreModel>(jjjson.ToString());
                        redTeamTextView.Text = "" + result.redTeamScore;
                        blueTeamTextView.Text = "" + result.blueTeamScore;

                    }
                    
                    return jsonDoc;
                }

            }
        }


        public string CreateDB()
        {
            var output = "";
            output += "Creating Databse if it doesnt exists";
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "scoresDB.db3"); //Create New Database  
            var db = new SQLiteConnection(dpPath);
            output += "\n Database Created....";
            return output;
        }

        public void insertEndResult()
        {
            try
            {
                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "scoresDB.db3");
                var db = new SQLiteConnection(dpPath);
                db.CreateTable<ScoreModel>();
                ScoreModel tbl = new ScoreModel();

                Toast.MakeText(Application.Context, " "+ Int64.Parse(GetTimestamp(DateTime.Now)), ToastLength.Short).Show();
                long id = Int64.Parse(GetTimestamp(DateTime.Now));
                db.Query<ScoreModel>(String.Format("INSERT INTO [ScoreModel] VALUES ({0}, 2, 2)", id));  //here will be final result
            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, ex.ToString(), ToastLength.Short).Show();
            }
        }
        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }


    }

}