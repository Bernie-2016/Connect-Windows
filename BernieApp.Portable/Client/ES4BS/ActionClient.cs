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
                Id = (int)e["_id"],
                Title = (string)e,
                Type = (string)e,
                Date = (string)e,
                ShortDescription = (string)e,
                Body = (string)e,
                BodyHTML = (string)e,
                TargetUrl = (string)e,
                TweetId = (int)e,
                TwitterUrl = (string)e
            }).ToList();

            return alerts as List<ActionAlert>;
        }

        public async Task<ActionAlert> GetAsync(int id)
        {
            string _id = id.ToString();
            string uri = String.Format("{0}/{1}", _endpoint, _id);

            var message = await WebRequest(uri);

            string json = await message.Content.ReadAsStringAsync();
            JObject httpResponse = JObject.Parse(json);
            IList<JToken> data = httpResponse["data"].Children().ToList();
            var alertsJson = JsonConvert.SerializeObject(data);
            JArray alertsArray = JArray.Parse(alertsJson);
            IList<ActionAlert> alerts = alertsArray.Select(e => new ActionAlert
            {
                Id = (int)e["_id"],
                Title = (string)e,
                Type = (string)e,
                Date = (string)e,
                ShortDescription = (string)e,
                Body = (string)e,
                BodyHTML = (string)e,
                TargetUrl = (string)e,
                TweetId = (int)e,
                TwitterUrl = (string)e
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
