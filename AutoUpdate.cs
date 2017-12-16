using System;
using System.Json;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Android.Widget;

namespace Foosball_Android
{
    public class AutoUpdate
    {

        public List<ScoreModel> scoreModel;
        private static System.Timers.Timer updateTimer;



        public AutoUpdate()
        { 
            scoreModel = new List<ScoreModel>();
        }






        public void setTimer()
        {
            updateTimer = new System.Timers.Timer();
            updateTimer.Elapsed += UpdateEventAsync;
            updateTimer.Interval = 5000;
            updateTimer.Enabled = true;
            updateTimer.AutoReset = true;
        }
        public  async void UpdateEventAsync(Object source, EventArgs e)
        {
            string url = "http://192.168.1.102:5000/api/scores";
            await Fetchdata(url);
        }

        private  async Task<JsonValue> Fetchdata(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";

            using (WebResponse response = await request.GetResponseAsync())
            {
                using (System.IO.Stream stream = response.GetResponseStream())
                {
                    
                    //ScoreFragment <string> scoreBox = new ScoreFragment<string>();
                    JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
                    var value = jsonDoc.Count;
                    
                    foreach (var jjjson in jsonDoc)
                    {
                        var result = JsonConvert.DeserializeObject<ScoreModel>(jjjson.ToString());

                        scoreModel.Add(result);
                        if(result.redTeamScore == 0 && result.blueTeamScore == 0)
                        {
                            showWhatYouGot();
                        }
                        Toast.MakeText(Android.App.Application.Context, "" + result.redTeamScore, ToastLength.Long).Show();
  //                      ScoreFragment<string>.redTeamTextView.Text = "" + result.redTeamScore;
  //                      ScoreFragment<string>.blueTeamTextView.Text = "" + result.blueTeamScore;
                    }
                    Console.WriteLine("Tick");
                    return jsonDoc;
                }
            }

        }

        public void showWhatYouGot()
        {
            foreach(var item in scoreModel)
            {
                Toast.MakeText(Android.App.Application.Context, "" + item.redTeamScore, ToastLength.Long).Show();
            }
        }

    }


}