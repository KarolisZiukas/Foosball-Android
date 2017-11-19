
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Json;
using System.Net;
using System;
using System.Threading.Tasks;
using Android.Media;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Foosball_Android
{


    [Activity(Label = "ScoreFragment")]
    public class ScoreFragment<T> : Fragment
    {
        MediaPlayer _mediaPlayer;
        TextView redTeamTextView;
        TextView blueTeamTextView;
        

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {



            View view = inflater.Inflate(Resource.Layout.ScoreFragment, null);
            redTeamTextView = view.FindViewById<TextView>(Resource.Id.red_team_text_view);
            blueTeamTextView = view.FindViewById<TextView>(Resource.Id.blue_team_text_view);

            //ToDo Karolis: await/async
            redTeamTextView.Click += async (sender, e) =>
            {

                string url = "http://172.26.21.75:5000/api/values";
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
                    JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
                    Toast.MakeText(this.Activity, jsonDoc.ToString(), ToastLength.Short).Show();
                    return jsonDoc;
                }

            }
        }
    }
}