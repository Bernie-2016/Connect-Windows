using BernieApp.Common.Models;

namespace BernieApp.Common.Http
{
    public class IssuesClient :FeedClient<IssuesQueryResponse, Issue>
    {
        public IssuesClient() : base(Endpoints.IssuesBaseUrl, new FeedFilter { Type = FeedItemType.Issues })
        {

        }
    }
}
