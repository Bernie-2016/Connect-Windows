using System;
using Newtonsoft.Json;

namespace BernieApp.Portable.Models
{
    public class FeedEntry
    {
        [JsonProperty(PropertyName = "news_id")]
        public string Id { get; set; }

        public string Title { get; set; }

        [JsonProperty(PropertyName = "timestamp_publish")]
        public DateTime Date { get; set; }

        public string Body { get; set; }
        public string Excerpt { get; set; }
        public string ImageUrl { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
    }
}
