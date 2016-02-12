using BernieApp.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace BernieApp.Common.Http
{
    public class BernieHttpClient : IBernieHttpClient
    {
        private readonly IssuesClient _issuesClient;
        private readonly NewsClient _newsClient;

        public BernieHttpClient(IssuesClient issuesClient, NewsClient newsClient)
        {
            _issuesClient = issuesClient;
            _newsClient = newsClient;
        }

        public async Task<IEnumerable<HitDataItem<NewsArticle>>> GetNewsAsync()
        {
            return await _newsClient.GetAsync();
        }

        public async Task<IEnumerable<HitDataItem<Issue>>> GetIssuesAsync()
        {
            return await _issuesClient.GetAsync();
        }

        public async Task<HitDataItem<NewsArticle>> GetNewsArticleAsync(string id)
        {
            return await _newsClient.GetByIdAsync(id);
        }
    }
}
