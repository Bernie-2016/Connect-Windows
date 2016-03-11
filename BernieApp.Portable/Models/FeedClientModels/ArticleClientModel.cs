using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BernieApp.Portable.Models.FeedClientModels
{
    public class ArticleClientModel
    {
        public int from { get; set; }
        public int size { get; set; }
        public string[] _source { get; set; }
        public Filter filter { get; set; }
    }

    public class Filter
    {
        public Term term { get; set; }
    }

    public class Term
    {
        public string _id { get; set; }
    }
}
