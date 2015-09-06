using System.Collections.Generic;
using System.Threading.Tasks;

namespace BernieApp.Common.Models
{
    public interface IBernieHttpClient
    {
        Task<IEnumerable<NewsArticle>> GetNewsAsync();

        Task<IEnumerable<Issue>> GetIssuesAsync();
    }
}
