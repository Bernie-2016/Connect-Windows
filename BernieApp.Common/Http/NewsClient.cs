using BernieApp.Common.Models;
using System.Collections.Generic;

namespace BernieApp.Common.Http
{
    public class NewsClient : ES4BSClient<NewsQueryResponse, NewsArticle>
    {
        public NewsClient() : base(Endpoints.SitesEN, 
            new List<UrlQueryParam>() {
                new UrlQueryParam() {
                    Field = "q",
                    Value = "article_type:News"
                }
            })
        {

        }
    }
}
