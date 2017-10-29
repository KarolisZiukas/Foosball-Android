
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Json;
using System.Net;
using System;
using System.IO;
using Android.Media;
using System.Threading.Tasks;

namespace Foosball_Android
{
    [Activity(Label = "ScoreFragment")]
    public class ScoreFragment : Fragment
    {
        MediaPlayer _mediaPlayer;
        TextView redTeamTextView;
        TextView blueTeamTextView;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {



            View view = inflater.Inflate(Resource.Layout.ScoreFragment, null);
            redTeamTextView = view.FindViewById<TextView>(Resource.Id.red_team_text_view);
            blueTeamTextView = view.FindViewById<TextView>(Resource.Id.blue_team_text_view);
            //redTeamTextView.Click += delegate
            //{
            //    _mediaPlayer = MediaPlayer.Create(Application.Context, Resource.Raw.red_team_scored);
            //    _mediaPlayer.Start();
            //};
            redTeamTextView.Click += async (sender, e) =>
            {
                string url = "http://private-c9b9e-foosball2.apiary-mock.com/foosballApp/questions";
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
                    Toast.MakeText(this.Activity,jsonDoc.ToString(), ToastLength.Short).Show();
                    return jsonDoc;
                }

            }
        }
    }
}