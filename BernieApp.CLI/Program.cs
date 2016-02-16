using BernieApp.Portable.Client;
using BernieApp.Portable.Client.ES4BS;
using BernieApp.Portable.Models;
using System;

namespace BernieApp.CLI {
    class Program
    {
        static void Main(string[] args) {
            FeedClient<NewsArticle> newsClient = new FeedClient<NewsArticle>(
                Endpoints.NewsBaseUrl,
                new FeedFilter { Type = FeedItemType.News });
            FeedClient<Issue> issuesClient = new FeedClient<Issue>(
                Endpoints.IssuesBaseUrl,
                new FeedFilter { Type = FeedItemType.Issues });
            var client = new LiveBernieClient(newsClient, issuesClient);
            var news = client.GetNewsAsync().Result;
            for (int i = 0; i < news.Count; i++)
            {
                var entry = news[i];
                Console.WriteLine(i + ": " + entry.Id + " " + entry.Title);
            }
            var n = int.Parse(Console.ReadLine());
            Console.Clear();
            Console.WriteLine(news[n].Title);
            Console.WriteLine(news[n].Body);
        }
    }
}
