using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace BernieApp.Portable.Client.ES4BS.DataTransferObjects
{
    public class ResponseDto<TDataType>
    {
        public HitDataDto<TDataType> Hits { get; set; }

        [JsonProperty(PropertyName = "timed_out")]
        public bool TimedOut { get; set; }

        public IEnumerable<TDataType> Items => Hits.Items.Select(x => x.Source);
    }
}
