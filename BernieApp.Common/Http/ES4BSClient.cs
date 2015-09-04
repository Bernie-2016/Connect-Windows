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
    public class ES4BSClient<TResponseType, TDataType> where TResponseType : ES4BSResponse<TDataType> where TDataType : ArticleData
    {
        private string _endpoint;
        private IEnumerable<UrlQueryParam> _requiredQueryParams;

        public ES4BSClient(string endpoint, IEnumerable<UrlQueryParam> requiredQueryParams = null)
        {
            _endpoint = endpoint;
            _requiredQueryParams = requiredQueryParams;
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
                    return JsonConvert.DeserializeObject<TResponseType>(result);
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
                var queryParamPairs =
                    from param in queryParams
                    select string.Format("{0}={1}", WebUtility.UrlEncode(param.Field), WebUtility.UrlEncode(param.Value));
                combinedParamPairs.Concat(queryParamPairs);
            }
        }

        public async Task<IEnumerable<TDataType>> GetAsync(IEnumerable<UrlQueryParam> queryParams = null)
        {
            var resp = await GetAsyncRaw(queryParams);

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
