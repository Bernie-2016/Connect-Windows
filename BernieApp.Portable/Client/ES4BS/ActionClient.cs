using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using BernieApp.Portable.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BernieApp.Portable.Client.ES4BS
{
    public class ActionClient<TDataType>
    {
        private readonly string _endpoint;

        public ActionClient(string endpoint)
        {
            _endpoint = endpoint;
        }

        public async Task<List<ActionAlert>> GetAsync()
        {
            var uri = _endpoint;

            var message = await WebRequest(uri);

            var json = await message.Content.ReadAsStringAsync();
            JObject httpResponse = JObject.Parse(json);
            IList<JToken> data = httpResponse["data"].Children().ToList();
            var alertsJson = JsonConvert.SerializeObject(data);
            JArray alertsArray = JArray.Parse(alertsJson);
            IList<ActionAlert> alerts = alertsArray.Select(e => new ActionAlert
            {
                Id = (string)e["id"],
                Title = (string)e["attributes"]["title"],
                Type = (string)e["type"],
                Date = (string)e["attributes"]["date"],
                ShortDescription = (string)e["attributes"]["short_description"],
                Body = (string)e["attributes"]["body"],
                BodyHTML = (string)e["attributes"]["body_html"],
                TargetUrl = (string)e["attributes"]["target_url"],
                TweetId = (string)e["attributes"]["tweet_id"],
                TwitterUrl = (string)e["attributes"]["twitter_url"]
            }).ToList();

            return alerts as List<ActionAlert>;
        }

        public async Task<ActionAlert> GetAsync(string id)
        {
            string _id = id;
            string uri = String.Format("{0}/{1}", _endpoint, _id);

            var message = await WebRequest(uri);

            string json = await message.Content.ReadAsStringAsync();
            JObject httpResponse = JObject.Parse(json);
            IList<JToken> data = httpResponse["data"].Children().ToList();
            var alertsJson = JsonConvert.SerializeObject(data);
            JArray alertsArray = JArray.Parse(alertsJson);
            IList<ActionAlert> alerts = alertsArray.Select(e => new ActionAlert
            {
                Id = (string)e["id"],
                Title = (string)e["attributes"]["title"],
                Type = (string)e["type"],
                Date = (string)e["attributes"]["date"],
                ShortDescription = (string)e["attributes"]["short_description"],
                Body = (string)e["attributes"]["body"],
                BodyHTML = (string)e["attributes"]["body_HTML"],
                TargetUrl = (string)e["attributes"]["target_url"],
                TweetId = (string)e["attributes"]["tweet_id"],
                TwitterUrl = (string)e["attributes"]["twitter_url"]
            }).ToList();
            ActionAlert alert = alerts.FirstOrDefault();

            return alert;
        }

        private async Task<HttpResponseMessage> WebRequest(string uri)
        {
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri(uri);
                //client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(uri); //Need to make this put in the identifier
                if (response.IsSuccessStatusCode)
                {
                    return response;
                }
                else
                {
                    //Do something
                    throw new HttpRequestException();
                }
            }
        }
    }
}
