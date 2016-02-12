using BernieApp.Common.Helpers;

namespace BernieApp.Common.Http
{
    public class FeedFilter
    {
        [FriendlyString("article_type")]
        public FeedItemType Type { get; set; } = FeedItemType.None;

        [FriendlyString("_id")]
        public string Id { get; set; }
    }

    public enum FeedItemType
    {
        [FriendlyString("*")] None,
        [FriendlyString("News")] News,
        [FriendlyString("Issues")] Issues,
    }
}