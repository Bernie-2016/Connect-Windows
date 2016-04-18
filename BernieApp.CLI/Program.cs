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
            ActionClient<ActionAlert> actionClient = new ActionClient<ActionAlert>(
                Endpoints.ActionAlertsUrl);

            var client = new LiveBernieClient(newsClient, actionClient);
            Console.WriteLine("NEWSFEED");
            Console.WriteLine("=====================================");
            var news = client.GetNewsAsync().Result;
            var alerts = client.GetActionsAsync().Result;
            for (int i = 0; i < news.Count; i++)
            {
                var entry = news[i];
                Console.WriteLine(i + ": " + entry.Id + " " + entry.Title);
            }
            Console.WriteLine("ACTION ALERTS");
            Console.WriteLine("=====================================");
            for (int i = 0; i < alerts.Count; i++)
            {
                var entry = alerts[i];
                Console.WriteLine(i + ": " + entry.Id + " " + entry.Title + " " + entry.ShortDescription);
            }
            Console.ReadLine();
        }
    }
}
