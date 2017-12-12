using System;
using System.Json;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Foosball_Android
{
    static class AutoUpdate
    {
        private static System.Timers.Timer updateTimer;
        public static void setTimer()
        {
            updateTimer = new System.Timers.Timer();
            updateTimer.Elapsed += UpdateEventAsync;
            updateTimer.Interval = 1000;
            updateTimer.Enabled = true;
            updateTimer.AutoReset = true;
        }
        public static async void UpdateEventAsync(Object source, EventArgs e)
        {
            string url = "http://192.168.1.102:5000/api/scores";
            await Fetchdata(url);
        }
        private static async Task<JsonValue> Fetchdata(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";

            using (WebResponse response = await request.GetResponseAsync())
            {
                using (System.IO.Stream stream = response.GetResponseStream())
                {
                    ScoreModel scoreModel = new ScoreModel();
                    //ScoreFragment <string> scoreBox = new ScoreFragment<string>();
                    JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));

                    foreach (var jjjson in jsonDoc)
                    {
                        var result = JsonConvert.DeserializeObject<ScoreModel>(jjjson.ToString());
                        ScoreFragment<string>.redTeamTextView.Text = "" + result.redTeamScore;
                        ScoreFragment<string>.blueTeamTextView.Text = "" + result.blueTeamScore;
                    }
                    Console.WriteLine("Tick");
                    return jsonDoc;
                }
            }

        }
    }
}