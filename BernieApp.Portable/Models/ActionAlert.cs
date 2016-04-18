using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BernieApp.Portable.Models
{
    public class ActionAlert
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "short_description")]
        public string ShortDescription { get; set; }

        [JsonProperty(PropertyName = "body")]
        public string Body { get; set; }

        [JsonProperty(PropertyName = "body_HTML")]
        public string BodyHTML { get; set; }

        [JsonProperty(PropertyName = "date")]
        public string Date { get; set; }

        [JsonProperty(PropertyName = "target_url")]
        public string TargetUrl { get; set; }

        [JsonProperty(PropertyName = "twitter_url")]
        public string TwitterUrl { get; set; }

        [JsonProperty(PropertyName = "tweet_id")]
        public string TweetId { get; set; }
    }
}
