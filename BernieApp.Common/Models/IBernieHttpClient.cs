using System.Collections.Generic;
using System.Threading.Tasks;

namespace BernieApp.Common.Models
{
    public interface IBernieHttpClient
    {
        Task<IEnumerable<HitDataItem<NewsArticle>>> GetNewsAsync();

        Task<IEnumerable<HitDataItem<Issue>>> GetIssuesAsync();
    }
}
