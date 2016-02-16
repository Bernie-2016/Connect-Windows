using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using BernieApp.Portable.Client.ES4BS.DataTransferObjects;
using BernieApp.Portable.Helpers;
using Newtonsoft.Json;

namespace BernieApp.Portable.Client.ES4BS
{
    public class FeedFilter
    {
        [FriendlyString("article_type")]
        public FeedItemType Type { get; set; } = FeedItemType.None;

        [FriendlyString("_id")]
        public string Id { get; set; }
    }

    public enum FeedItemType
    {
        [FriendlyString("*")] None,
        [FriendlyString("News")] News,
        [FriendlyString("Issues")] Issues,
    }

    public class FeedClient<TDataType>
    {
        private readonly string _endpoint;
        private readonly FeedFilter _defaultFilters;

        public FeedClient(string endpoint, FeedFilter defaultFilters = null)
        {
            _endpoint = endpoint;
            _defaultFilters = defaultFilters;
        }

        public async Task<List<TDataType>> GetAsync(FeedFilter filters = null)
        {
            var entries = await GetEntriesAsync(filters);
            return entries;
        }

        public async Task<TDataType> GetByIdAsync(string id)
        {
            var filter = new FeedFilter { Id = id };
            var entries = await GetEntriesAsync(filter);
            return entries.FirstOrDefault();
        }

        private async Task<List<TDataType>> GetEntriesAsync(FeedFilter filters)
        {
            var queryStr = BuildQueryString(filters);
            var uri = new Uri(_endpoint + queryStr);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                var json = await client.GetStringAsync(uri);
                var result = default(ResponseDto<TDataType>);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    result = JsonConvert.DeserializeObject<ResponseDto<TDataType>>(json);
                }
                return result.Items.ToList();
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
