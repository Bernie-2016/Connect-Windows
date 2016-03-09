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
using BernieApp.Portable.Models.FeedClientModels;
using Newtonsoft.Json;

namespace BernieApp.Portable.Client.ES4BS
{

    public class FeedClient<TDataType>
    {
        private readonly string _endpoint;

        public FeedClient(string endpoint)
        {
            _endpoint = endpoint;
        }

        public async Task<List<TDataType>> GetAsync()
        {
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

            var response = await GetEntriesAsync(content);

            //Parse Json here
            var json = await response.Content.ReadAsStringAsync();
            var entries = JsonConvert.DeserializeObject<ResponseDto<TDataType>>(json).Items.ToList();

            return entries;
        }

        //public async Task<TDataType> GetAsync(string id)
        //{
        //    //var entry = await GetEntryAsync(id);

        //    //Parse Json here

        //    //return entry
        //}

        private async Task<HttpResponseMessage> GetEntriesAsync(string queryString)
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

        //private async Task GetEntryAsync(string id)
        //{
        //    var result = string.Empty;
        //    return result;
        //}
    }
}
