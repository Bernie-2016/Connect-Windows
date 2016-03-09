using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BernieApp.Portable.Models.FeedClientModels
{
    public class ArticlesClientModel
    {
        public int from { get; set; }
        public int size { get; set; }
        public string[] _source { get; set; }
        public Query query { get; set; }
        public Sort sort { get; set; }
    }

    public class Query
    {
        public QueryString query_string { get; set; }
    }

    public class QueryString
    {
        public string default_field { get; set; }
        public string query { get; set; }
    }

    public class Sort
    {
        public TimeStampPublish timestamp_publish { get; set; }
    }

    public class TimeStampPublish
    {
        public string order { get; set; }
        public bool ignore_unmapped { get; set; }
    }
}
