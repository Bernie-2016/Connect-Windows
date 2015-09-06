using BernieApp.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BernieApp.Common.Http
{
    public class BernieHttpClient : IBernieHttpClient
    {
        private IssuesClient _issuesClient;
        private NewsClient _newsClient;

        public BernieHttpClient()
        {
            _newsClient = new NewsClient();
            _issuesClient = new IssuesClient();
        }

        public async Task<IEnumerable<NewsArticle>> GetNewsAsync()
        {
            return await _newsClient.GetAsync();
        }

        public async Task<IEnumerable<Issue>> GetIssuesAsync()
        {
            return await _issuesClient.GetAsync();
        }
    }
}
