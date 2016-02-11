using Newtonsoft.Json;
using System.Collections.Generic;

namespace BernieApp.Common.Models
{
    public class HitData<TDataType>
    {
        [JsonProperty(PropertyName = "hits")]
        public IEnumerable<HitDataItem<TDataType>> Items { get; set; }

        [JsonProperty(PropertyName = "max_score", NullValueHandling = NullValueHandling.Ignore)]
        public double MaxScore { get; set; }

        public int Total { get; set; }
    }
}
