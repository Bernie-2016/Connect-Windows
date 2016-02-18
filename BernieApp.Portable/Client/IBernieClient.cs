using BernieApp.Portable.Client.ES4BS;
using BernieApp.Portable.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BernieApp.Portable.Client {
    public interface IBernieClient
    {
        Task<List<FeedEntry>> GetNewsAsync();
        Task<FeedEntry> GetNewsArticleAsync(string id);
        Task<List<FeedEntry>> GetIssuesAsync();
    }

    public class LiveBernieClient : IBernieClient
    {
        private readonly FeedClient<FeedEntry> _newsClient;
        private readonly FeedClient<FeedEntry> _issuesClient;

        public LiveBernieClient(FeedClient<FeedEntry> newsClient, FeedClient<FeedEntry> issuesClient)
        {
            _newsClient = newsClient;
            _issuesClient = issuesClient;
        }

        public Task<List<FeedEntry>> GetNewsAsync() => _newsClient.GetAsync();

        public Task<FeedEntry> GetNewsArticleAsync(string id) => _newsClient.GetByIdAsync(id);

        public Task<List<FeedEntry>> GetIssuesAsync() => _issuesClient.GetAsync();
    }

    public class DesignTimeBernieClient : IBernieClient
    {
        public async Task<List<FeedEntry>> GetNewsAsync()
        {
            return new List<FeedEntry>();
        }

        public async Task<FeedEntry> GetNewsArticleAsync(string id)
        {
            return new FeedEntry();
        }

        public async Task<List<FeedEntry>> GetIssuesAsync()
        {
            return new List<FeedEntry>();
        }
    }
}
