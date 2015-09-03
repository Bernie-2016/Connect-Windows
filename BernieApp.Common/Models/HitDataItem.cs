using Newtonsoft.Json;

namespace BernieApp.Common.Models
{
    public class HitDataItem<TDataType>
    {
        [JsonProperty(PropertyName = "_id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "_index")]
        public string Index { get; set; }

        [JsonProperty(PropertyName = "_score")]
        public string Score { get; set; }

        [JsonProperty(PropertyName = "_source")]
        public TDataType Source { get; set; }

        [JsonProperty(PropertyName = "_type")]
        public string Type { get; set; }
    }
}
