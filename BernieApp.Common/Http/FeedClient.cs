using BernieApp.Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using BernieApp.Common.Helpers;

namespace BernieApp.Common.Http {
    public class FeedClient<TResponseType, TDataType> 
        where TResponseType : ES4BSResponse<TDataType> 
        where TDataType : ArticleData        
    {
        private readonly string _endpoint;
        private readonly FeedFilter _defaultFilters;

        public FeedClient(string endpoint, FeedFilter defaultFilters = null)
        {
            _endpoint = endpoint;
            _defaultFilters = defaultFilters;
        }

        public async Task<IEnumerable<HitDataItem<TDataType>>> GetAsync(FeedFilter filters = null)
        {
            var resp = await GetAsyncRaw(filters);
            return resp.Hits.Items.ToList();
        }

        public async Task<HitDataItem<TDataType>> GetByIdAsync(string id)
        {
            var filter = new FeedFilter { Id = id };
            var resp = await GetAsyncRaw(filter);
            return resp.Hits.Items.FirstOrDefault();
        }

        private async Task<TResponseType> GetAsyncRaw(FeedFilter filters)
        {
            var queryStr = BuildQueryString(filters);
            var uri = new Uri(_endpoint + queryStr);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                var json = await client.GetStringAsync(uri);
                var result = default(TResponseType);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    result = JsonConvert.DeserializeObject<TResponseType>(json);
                }
                return result;
            }
        }

        private object BuildQueryString(FeedFilter filters)
        {
            var dict = new Dictionary<string, string>();
            BuildQueryStringHelper(dict, _defaultFilters);
            BuildQueryStringHelper(dict, filters);
            return "?(" + string.Join(") AND (", dict.Select(kvp => $"{kvp.Key}={kvp.Value}")) + ")";
        }

        private void BuildQueryStringHelper(Dictionary<string, string> dict, FeedFilter filter)
        {
            if (filter == null)
            {
                return;
            }
            var feedFilterType = filter.GetType().GetTypeInfo();
            foreach (var property in feedFilterType.DeclaredProperties)
            {
                object value = property.GetValue(filter);

                if (value != null)
                {
                    var valueFriendlyStringAttribute = value.GetEnumAttribute<FriendlyStringAttribute>();
                    if (valueFriendlyStringAttribute != null)
                    {
                        value = valueFriendlyStringAttribute.Value;
                    }

                    var keyString = property.GetCustomAttribute<FriendlyStringAttribute>().Value;
                    dict.Add(keyString, value.ToString());
                }
            }
        }
    }
}
