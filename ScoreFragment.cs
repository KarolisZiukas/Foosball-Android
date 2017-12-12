using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Media;

namespace Foosball_Android
{


    [Activity(Label = "ScoreFragment")]
    public class ScoreFragment <T> : Fragment
    {
        MediaPlayer _mediaPlayer;
        public static TextView redTeamTextView { get; set; }
        public static TextView blueTeamTextView { get; set; }
        //Button openDataTableBt;
        //Button openDataBaseBt;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.ScoreFragment, null);
            redTeamTextView = view.FindViewById<TextView>(Resource.Id.red_team_text_view);
            blueTeamTextView = view.FindViewById<TextView>(Resource.Id.blue_team_text_view);
            AutoUpdate.setTimer();
            //ToDo Karolis: await/async
           /* redTeamTextView.Click += async (sender, ElapsedEventArgs e) =>
            {

                string url = "http://192.168.1.102:5000/api/scores";
                JsonValue json = await Fetchdata(url);

            };*/

            blueTeamTextView.Click += delegate
            {          
                _mediaPlayer = MediaPlayer.Create(Application.Context, Resource.Raw.blue_team_scored);
                _mediaPlayer.Start();
            };

            return view;
        }
    }
}