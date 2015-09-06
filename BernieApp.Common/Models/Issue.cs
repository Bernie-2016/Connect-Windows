using Newtonsoft.Json;

namespace BernieApp.Common.Models
{
    public class Issue : ArticleData
    {
        public string Body { get; set; }

        [JsonProperty(PropertyName = "body_html")]
        public string BodyHtml { get; set; }

        public string Charset { get; set; }

        public int ContentLength { get; set; }

        public string Description { get; set; }

        [JsonProperty(PropertyName = "img_url")]
        public string ImgUrl { get; set; }

        public string Locale { get; set; }

        public string MimeType { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }
    }
}
