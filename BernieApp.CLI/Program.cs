using BernieApp.Portable.Client;
using BernieApp.Portable.Client.ES4BS;
using BernieApp.Portable.Models;
using System;

namespace BernieApp.CLI {
    class Program
    {
        static void Main(string[] args) {
            FeedClient<FeedEntry> newsClient = new FeedClient<FeedEntry>(
                Endpoints.NewsBaseUrl);

            var client = new LiveBernieClient(newsClient);
            var news = client.GetNewsAsync().Result;
            for (int i = 0; i < news.Count; i++)
            {
                var entry = news[i];
                Console.WriteLine(i + ": " + entry.Id + " " + entry.Title);
            }
            
        }
    }
}
