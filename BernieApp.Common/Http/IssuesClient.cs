using BernieApp.Common.Models;
using System.Collections.Generic;

namespace BernieApp.Common.Http
{
    public class IssuesClient :ES4BSClient<IssuesQueryResponse, Issue>
    {
        public IssuesClient() : base(Endpoints.SitesEN,
            new List<UrlQueryParam>() {
                new UrlQueryParam() {
                    Field = "q",
                    Value = "article_type:Issues"
                }
            })
        {

        }
    }
}
