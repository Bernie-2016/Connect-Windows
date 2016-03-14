using System;
using Newtonsoft.Json;

namespace BernieApp.Portable.Models
{
    public class FeedEntry
    {
        [JsonProperty(PropertyName = "uuid")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "article_type")]
        public string ArticleType { get; set; }

        [JsonProperty(PropertyName = "timestamp_publish")]
        public DateTime Date { get; set; }

        [JsonProperty(PropertyName = "body")]
        public string Body { get; set; }

        [JsonProperty(PropertyName = "excerpt")]
        public string Excerpt { get; set; }

        [JsonProperty(PropertyName = "img_url")]
        public string ImageUrl { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName ="lang")]
        public string Language { get; set; }
    }
}
