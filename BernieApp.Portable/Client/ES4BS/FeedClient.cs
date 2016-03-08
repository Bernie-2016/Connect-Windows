﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using BernieApp.Portable.Client.ES4BS.DataTransferObjects;
using BernieApp.Portable.Helpers;
using BernieApp.Portable.Models;
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
            string query;
            using (var content = new FormUrlEncodedContent(new KeyValuePair<string, >[]{
                new KeyValuePair<string, string>("from", "0"),
                new KeyValuePair<string, string>("size", "30"),
                new KeyValuePair<string, List<string>("_source", System.Collections.Generic.List<string> = {"title", "body_markdown", "excerpt", "timestamp_publish", "url", "image_url" }),
            }))
            {
                query = content.ReadAsStringAsync().Result;
            }

            var response = await GetEntriesAsync(query);

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

        private async Task<HttpResponseMessage> GetEntriesAsync(string query)
        {
            var uri = new Uri(_endpoint);
            var content = query; //Need to cast to HttpContent

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
