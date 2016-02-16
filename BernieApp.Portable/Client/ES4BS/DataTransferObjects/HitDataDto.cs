using System.Collections.Generic;
using Newtonsoft.Json;

namespace BernieApp.Portable.Client.ES4BS.DataTransferObjects
{
    public class HitDataDto<TDataType>
    {
        [JsonProperty(PropertyName = "hits")]
        public List<HitDataItemDto<TDataType>> Items { get; set; }

        public int Total { get; set; }
    }
}
