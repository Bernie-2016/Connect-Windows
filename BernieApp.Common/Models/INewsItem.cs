using System;

namespace BernieApp.Common.Models
{
    public interface INewsItem
    {
        string Body { get; set; }

        string BodyHtml { get; set; }

        string Charset { get; set; }

        int ContentLength { get; set; }

        string Description { get; set; }
       
        string ImgUrl { get; set; }

        string Locale { get; set; }

        string MimeType { get; set; }
        
        DateTime PublishedTime { get; set; }

        string Title { get; set; }

        string Url { get; set; }
    }
}
