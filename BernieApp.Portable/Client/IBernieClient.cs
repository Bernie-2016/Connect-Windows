using BernieApp.Portable.Client.ES4BS;
using BernieApp.Portable.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BernieApp.Portable.Client {
    public interface IBernieClient
    {
        Task<List<FeedEntry>> GetNewsAsync();
        Task<FeedEntry> GetNewsArticleAsync(string id);
        Task<List<ActionAlert>> GetActionsAsync();
        Task<ActionAlert> GetActionAsync(string id);
    }

    public class LiveBernieClient : IBernieClient
    {
        private readonly FeedClient<FeedEntry> _newsClient;
        private readonly ActionClient<ActionAlert> _actionClient;

        public LiveBernieClient(FeedClient<FeedEntry> newsClient, ActionClient<ActionAlert> actionClient)
        {
            _newsClient = newsClient;
            _actionClient = actionClient;

        }

        public Task<List<FeedEntry>> GetNewsAsync() => _newsClient.GetAsync();

        public Task<FeedEntry> GetNewsArticleAsync(string id) => _newsClient.GetAsync(id);

        public Task<List<ActionAlert>> GetActionsAsync() => _actionClient.GetAsync();

        public Task<ActionAlert> GetActionAsync(string id) => _actionClient.GetAsync(id);

    }

    public class DesignTimeBernieClient : IBernieClient
    {
        public async Task<List<FeedEntry>> GetNewsAsync()
        {
            return new List<FeedEntry>();
        }

        public async Task<FeedEntry> GetNewsArticleAsync(string id)
        {
            var entry = new FeedEntry
            {
                Id = "testId",
                Title = "Bernie wins the Election!",
                Date = DateTime.Now,
                ArticleType = "Press Release",
                Body = "This is a test article. Not a real article.... yet. The red fox jumps over the I can't actually remember the rest of the font sequence, so here is some more ranting since I need a sizable article body to accurately see in the Design Time client. That should be enough.",
                Excerpt = "A riveting test article",
                ImageUrl = "http://www.someimagelinkhere.com/image-test",
                Language = "en",
                Url = "http://www.BernieSanders.com/article-url"
            };
            return entry;
        }

        public async Task<List<ActionAlert>> GetActionsAsync()
        {
            return new List<ActionAlert>();
        }

        public async Task<ActionAlert> GetActionAsync(string id)
        {
            var alert = new ActionAlert
            {
                Id = "1",
                Title = "Look, a tweet!",
                Date = "March 23",
                ShortDescription = "Bleh",
                Body = "Body"

            };

            return alert;
        }
    }
}
