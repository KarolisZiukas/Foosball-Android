
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

                FragmentTransaction fragmentTransaction = FragmentManager.BeginTransaction();
                fragmentTransaction.Add(Resource.Id.main_frame_layout, new ScoreListFragment()).AddToBackStack(null);
                fragmentTransaction.Commit();
            };


            blueTeamTextView.Click += delegate
            {
                //_mediaPlayer = MediaPlayer.Create(Application.Context, Resource.Raw.blue_team_scored);
                //_mediaPlayer.Start();
            };

            return view;
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
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "scores.db3"); //Create New Database  
            var db = new SQLiteConnection(dpPath);
            output += "\n Database Created....";
            return output;
        }

        public void insertEndResult()
        {
            try
            {
                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "scores.db3");
                var db = new SQLiteConnection(dpPath);
                db.CreateTable<ScoreModel>();
                ScoreModel tbl = new ScoreModel();
                db.Query<ScoreModel>("INSERT INTO [ScoreModel] VALUES (1, 5, 6)");  //here will be final result
                Toast.MakeText(Application.Context, "Score added", ToastLength.Short).Show();
            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, ex.ToString(), ToastLength.Short).Show();
            }
        }



    }

}