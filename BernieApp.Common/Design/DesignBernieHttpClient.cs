using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BernieApp.Common.Models;

namespace BernieApp.Common.Design
{
    public class DesignBernieHttpClient : IBernieHttpClient
    {
        public Task<IEnumerable<Issue>> GetIssuesAsync()
        {
            return null;
        }

        public Task<IEnumerable<NewsArticle>> GetNewsAsync()
        {
            return null;
        }
    }
}
