using Newtonsoft.Json;

namespace BernieApp.Common.Models
{
    public class ES4BSResponse<TDataType> where TDataType : ArticleData
    {
        [JsonProperty(PropertyName = "_shards")]
        public ShardInfo Shards { get; set; }

        public HitData<TDataType> Hits { get; set; }

        [JsonProperty(PropertyName = "timed_out")]
        public bool TimedOut { get; set; }

        public int Took { get; set; }
    }
}
