using BernieApp.Common.Models;

namespace BernieApp.Common.Http
{
    public class IssuesClient :ES4BSClient<IssuesQueryResponse, Issue>
    {
        public IssuesClient() : base(Endpoints.SitesEN, "article_type:Issues")
        {

        }
    }
}
