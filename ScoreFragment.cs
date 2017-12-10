
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
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Foosball_Android
{


    [Activity(Label = "ScoreFragment")]
    public class ScoreFragment<T> : Fragment
    {
        MediaPlayer _mediaPlayer;
        TextView redTeamTextView;
        TextView blueTeamTextView;
        Button openDataTableBt;
        Button openDataBaseBt;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {



            View view = inflater.Inflate(Resource.Layout.ScoreFragment, null);
            redTeamTextView = view.FindViewById<TextView>(Resource.Id.red_team_text_view);
            blueTeamTextView = view.FindViewById<TextView>(Resource.Id.blue_team_text_view);

            //ToDo Karolis: await/async
            redTeamTextView.Click += async (sender, e) =>
            {

                string url = "http://192.168.1.102:5000/api/scores";
                JsonValue json = await Fetchdata(url);

            };

            blueTeamTextView.Click += delegate
            {          
                _mediaPlayer = MediaPlayer.Create(Application.Context, Resource.Raw.blue_team_scored);
                _mediaPlayer.Start();
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

       
    }
}