using BernieApp.Common.Models;

namespace BernieApp.Common.Http
{
    public class NewsClient : ES4BSClient<NewsQueryResponse, NewsArticle>
    {
        public NewsClient() : base("News")
        {

        }
    }
}
