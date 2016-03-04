using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using BernieApp.Portable.Client.ES4BS.DataTransferObjects;
using BernieApp.Portable.Helpers;
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
            var entries = await GetEntriesAsync();
            return entries;
        }

        public async Task GetByIdAsync(string id)
        {
            //var entries = await GetEntryAsync(id);
            //return entries.FirstOrDefault();
        }

        private async Task<List<TDataType>> GetEntriesAsync()
        {
            var uri = new Uri(_endpoint);

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsync(uri, new );
                if (response.IsSuccessStatusCode)
                {
                    
                }


                //var json = await client.GetStringAsync(uri).ConfigureAwait(false);
                //var result = default(ResponseDto<TDataType>);
                //if (!string.IsNullOrWhiteSpace(json))
                //{
                //    result = JsonConvert.DeserializeObject<ResponseDto<TDataType>>(json);
                //}
                //return result.Items.ToList();
            }
        }

        //private async Task GetEntryAsync(string id)
        //{
        //    var result = string.Empty;
        //    return result;
        //}
    }
}
