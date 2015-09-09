using System;

namespace BernieApp.Common.Models
{
    public class NewsItem : INewsItem
    {
        public string Id { get; set; }

        public string Body { get; set; }

        public string BodyHtml { get; set; }

        public string Charset { get; set; }

        public int ContentLength { get; set; }

        public string Description { get; set; }

        public string ImgUrl { get; set; }

        public string Locale { get; set; }

        public string MimeType { get; set; }

        public DateTime PublishedTime { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }
    }
}
