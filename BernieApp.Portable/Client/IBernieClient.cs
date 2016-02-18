using BernieApp.Portable.Client.ES4BS;
using BernieApp.Portable.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BernieApp.Portable.Client {
    public interface IBernieClient
    {
        Task<List<NewsArticle>> GetNewsAsync();
        Task<NewsArticle> GetNewsArticleAsync(string id);
        Task<List<Issue>> GetIssuesAsync();
    }

    public class LiveBernieClient : IBernieClient
    {
        private readonly FeedClient<NewsArticle> _newsClient;
        private readonly FeedClient<Issue> _issuesClient;

        public LiveBernieClient(FeedClient<NewsArticle> newsClient, FeedClient<Issue> issuesClient)
        {
            _newsClient = newsClient;
            _issuesClient = issuesClient;
        }

        public Task<List<NewsArticle>> GetNewsAsync() => _newsClient.GetAsync();

        public Task<NewsArticle> GetNewsArticleAsync(string id) => _newsClient.GetByIdAsync(id);

        public Task<List<Issue>> GetIssuesAsync() => _issuesClient.GetAsync();
    }

    public class DesignTimeBernieClient : IBernieClient
    {
        public async Task<List<NewsArticle>> GetNewsAsync()
        {
            return new List<NewsArticle>();
        }

        public async Task<NewsArticle> GetNewsArticleAsync(string id)
        {
            return new NewsArticle();
        }

        public async Task<List<Issue>> GetIssuesAsync()
        {
            return new List<Issue>();
        }
    }
}
