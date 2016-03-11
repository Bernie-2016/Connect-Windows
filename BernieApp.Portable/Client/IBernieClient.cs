using BernieApp.Portable.Client.ES4BS;
using BernieApp.Portable.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BernieApp.Portable.Client {
    public interface IBernieClient
    {
        Task<List<FeedEntry>> GetNewsAsync();
        Task<FeedEntry> GetNewsArticleAsync(string id);
    }

    public class LiveBernieClient : IBernieClient
    {
        private readonly FeedClient<FeedEntry> _newsClient;

        public LiveBernieClient(FeedClient<FeedEntry> newsClient)
        {
            _newsClient = newsClient;

        }

        public Task<List<FeedEntry>> GetNewsAsync() => _newsClient.GetAsync();

        public Task<FeedEntry> GetNewsArticleAsync(string id) => _newsClient.GetAsync(id);

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

    }
}
