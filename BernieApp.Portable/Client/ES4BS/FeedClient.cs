using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using BernieApp.Portable.Client.ES4BS.DataTransferObjects;
using BernieApp.Portable.Helpers;
using BernieApp.Portable.Models;
using BernieApp.Portable.Models.FeedClientModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BernieApp.Portable.Client.ES4BS
{

    public class FeedClient<TDataType>
    {
        private readonly string _endpoint;

        public FeedClient(string endpoint)
        {
            _endpoint = endpoint;
        }

        public async Task<List<FeedEntry>> GetAsync()
        {
            //Build Query
            var queryString = new ArticlesClientModel
            {
                from = 0,
                size = 30,
                _source = new[] { "uuid", "title", "article_type", "body", "excerpt", "timestamp_publish", "url", "image_url", "lang" },
                query = new Query
                {
                    query_string = new QueryString
                    {
                        default_field = "article_type",
                        query = "PressRelease OR DemocracyDaily"
                    }
                },
                sort = new Sort
                {
                    timestamp_publish = new TimeStampPublish
                    {
                        order = "desc",
                        ignore_unmapped = true
                    }
                }
            };
            string content = JsonConvert.SerializeObject(queryString, Formatting.Indented);

            var response = await WebRequest(content);

            //Convert to JObject to bypass Response/HitData classes, then convert to List
            var json = await response.Content.ReadAsStringAsync();
            JObject httpResponse = JObject.Parse(json);
            IList<JToken> httpResponseData = httpResponse["hits"]["hits"].Children().ToList();
            var hits = JsonConvert.SerializeObject(httpResponseData);
            JArray hitsArray = JArray.Parse(hits);
            IList<FeedEntry> entries = hitsArray.Select(e => new FeedEntry
            {
                Id = (string)e["_id"],
                Title = (string)e["_source"]["title"],
                ArticleType = (string)e["_source"]["article_type"],
                Date = (DateTime)e["_source"]["timestamp_publish"],
                Body = (string)e["_source"]["body"],
                Excerpt = (string)e["_source"]["excerpt"],
                Url = (string)e["_source"]["url"],
                ImageUrl = (string)e["_source"]["image_url"],
                Language = (string)e["_source"]["lang"]

            }).ToList();

            return entries as List<FeedEntry>;
        }

        public async Task<FeedEntry> GetAsync(string id)
        {
            //Create Query
            var queryString = new ArticleClientModel
            {
                from = 0,
                size = 1,
                _source = new[] { "uuid", "title", "article_type", "body", "excerpt", "timestamp_publish", "url", "image_url", "lang" },
                filter = new Filter
                {
                    term = new Term
                    {
                        _id = id
                    }
                }
            };
            string content = JsonConvert.SerializeObject(queryString, Formatting.Indented);

            var response = await WebRequest(content);

            //Parse Json
            var json = await response.Content.ReadAsStringAsync();
            JObject httpResponse = JObject.Parse(json);
            FeedEntry entry = new FeedEntry();

            return entry as FeedEntry;
        }

        private async Task<HttpResponseMessage> WebRequest(string queryString)
        {
            var uri = new Uri(_endpoint);
            var content = new StringContent(queryString); //Need to cast to HttpContent

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsync(uri, content);

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
