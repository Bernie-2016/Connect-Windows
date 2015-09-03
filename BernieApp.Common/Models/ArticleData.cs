using Newtonsoft.Json;
using System;

namespace BernieApp.Common.Models
{
    public class ArticleData
    {
        [JsonProperty(PropertyName = "@timestamp")]
        public DateTime TimeStamp { get; set; }

        [JsonProperty(PropertyName = "article_type")]
        public string ArticleType { get; set; }

        public int ExecutionTime { get; set; }

        public int HttpStatusCode { get; set; }

        public string Method { get; set; }

        public string ParentUrl { get; set; }
    }
}
