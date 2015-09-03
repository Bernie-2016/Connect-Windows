using BernieApp.Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BernieApp.Common.Http
{
    public class ES4BSClient<TResponseType, TDataType> where TResponseType : ES4BSResponse<TDataType> where TDataType : ArticleData
    {
        private const string ENDPOINT = "http://search.berniesanders.tech/sites_en/official/_search";
        private string _articleType;

        public ES4BSClient(string articleType)
        {
            _articleType = articleType;
        }

        private async Task<TResponseType> GetAsyncRaw()
        {
            var uri = new Uri(ENDPOINT + "?q=article_type:" + _articleType);

            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(uri))
            using (HttpContent content = response.Content)
            {
                string result = await content.ReadAsStringAsync();

                if (result != null)
                {
                    return JsonConvert.DeserializeObject<TResponseType>(result);
                }
                else
                {
                    return default(TResponseType);
                }
            }
        }

        public async Task<IEnumerable<TDataType>> GetAsync()
        {
            var resp = await GetAsyncRaw();

            var result = new List<TDataType>();
            foreach (var item in resp.Hits.Items)
            {
                var source = item.Source;
                result.Add(source);
            }

            return result;
        }
    }
}
