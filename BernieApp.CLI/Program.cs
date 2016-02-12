using System;
using System.Linq;
using System.Threading.Tasks;
using BernieApp.Common.DependencyInjection;
using BernieApp.Common.Http;

namespace BernieApp.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var diService = new AutofacDIService();
            var client = diService.Resolve<IBernieHttpClient>();
            var issues = client.GetIssuesAsync().Result.ToList();
            for (int i = 0; i < issues.Count; i++)
            {
                var issue = issues[i];
                Console.WriteLine(i + ": " + issue.Id + " " + issue.Index + " " + issue.Score + " " + issue.Type +
                                  issue.Source.Title);
            }
            var n = int.Parse(Console.ReadLine());
            Console.Clear();
            Console.WriteLine(issues[n].Source.Title);
            Console.WriteLine(issues[n].Source.Body);
        }
    }
}
