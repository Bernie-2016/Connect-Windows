using BernieApp.Common.Models;

namespace BernieApp.Common.Http
{
    public class NewsClient : FeedClient<NewsQueryResponse, NewsArticle>
    {
        public NewsClient() : base(Endpoints.NewsBaseUrl, new FeedFilter { Type = FeedItemType.News })
        {

        }
    }
}
