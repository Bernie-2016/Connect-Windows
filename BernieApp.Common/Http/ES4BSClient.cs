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
        private IEnumerable<UrlQueryParam> _requiredQueryParams;

        public ES4BSClient(string endpoint, IEnumerable<UrlQueryParam> requiredQueryParams = null)
        {
            _endpoint = endpoint;
            _requiredQueryParams = requiredQueryParams;
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

        private async Task<TResponseType> GetAsyncRaw(IEnumerable<UrlQueryParam> queryParams)
        {
            var queryStr = GetQueryString(queryParams);
            var uri = new Uri(_endpoint + queryStr);

            using (HttpClient client = new HttpClient())
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

        private string GetQueryString(IEnumerable<UrlQueryParam> queryParams)
        {
            var combinedParamPairs = new List<string>();

            CombineQueryParams(_requiredQueryParams, combinedParamPairs);
            CombineQueryParams(queryParams, combinedParamPairs);

            return combinedParamPairs.Count > 0 ? "?" + string.Join("&", combinedParamPairs) : "";
        }

        private static void CombineQueryParams(IEnumerable<UrlQueryParam> queryParams, List<string> combinedParamPairs)
        {
            if (queryParams != null)
            {
                foreach(var param in queryParams)
                {
                    var pair = string.Format("{0}={1}", WebUtility.UrlEncode(param.Field), WebUtility.UrlEncode(param.Value));
                    combinedParamPairs.Add(pair);
                }
            }
        }
    }
}
