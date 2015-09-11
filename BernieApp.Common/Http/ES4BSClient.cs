using BernieApp.Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace BernieApp.Common.Http
{
    public class ES4BSClient<TResponseType, TDataType> 
        where TResponseType : ES4BSResponse<TDataType> 
        where TDataType : ArticleData        
    {
        private string _endpoint;
        private string _defaultQueryFilter;

        public ES4BSClient(string endpoint, string defaultQueryFilter = null)
        {
            _endpoint = endpoint;
            _defaultQueryFilter = defaultQueryFilter;
        }

        public async Task<IEnumerable<HitDataItem<TDataType>>> GetAsync(IEnumerable<UrlQueryParam> queryParams = null)
        {
            var resp = await GetAsyncRaw(queryParams);

            var result = new List<HitDataItem<TDataType>>();
            foreach (var item in resp.Hits.Items)
            {
                result.Add(item);
            }

            return result;
        }

        public async Task<HitDataItem<TDataType>> GetByIdAsync(IEnumerable<UrlQueryParam> queryParams = null)
        {
            var resp = await GetAsyncRaw(queryParams);

            var result = new List<HitDataItem<TDataType>>();
            var item = resp.Hits.Items.FirstOrDefault();

            return item;
        }

        private async Task<TResponseType> GetAsyncRaw(IEnumerable<UrlQueryParam> queryParams)
        {
            var queryStr = ComputeQueryString(queryParams);
            var uri = new Uri(_endpoint + queryStr);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                using (HttpResponseMessage response = await client.GetAsync(uri))
                using (HttpContent content = response.Content)
                {
                    string result = await content.ReadAsStringAsync();

                    if (result != null)
                    {
                        var data = JsonConvert.DeserializeObject<TResponseType>(result);
                        return data;
                    }
                    else
                    {
                        return default(TResponseType);
                    }
                }
            }
        }

        private string ComputeQueryString(IEnumerable<UrlQueryParam> queryParams)
        {
            var combinedParamPairs = new List<string>();

            var addedQueryField = false;

            if (queryParams != null)
            {
                foreach(var param in queryParams)
                {
                    var value = param.Value;
                    if (param.Field == "q" && _defaultQueryFilter != null)
                    {
                        value = "(" + _defaultQueryFilter + ") AND (" + value + ")";
                        addedQueryField = true;
                    }
                    var pair = string.Format("{0}={1}", WebUtility.UrlEncode(param.Field), WebUtility.UrlEncode(value));
                    combinedParamPairs.Add(pair);
                }
            }

            if (!addedQueryField && _defaultQueryFilter != null)
            {
                var pair = string.Format("{0}={1}", WebUtility.UrlEncode("q"), WebUtility.UrlEncode(_defaultQueryFilter));
                combinedParamPairs.Add(pair);
            }

            return combinedParamPairs.Count > 0 ? "?" + string.Join("&", combinedParamPairs) : "";
        }
    }
}
