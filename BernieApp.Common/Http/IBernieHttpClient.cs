using System.Collections.Generic;
using System.Threading.Tasks;
using BernieApp.Common.Models;

namespace BernieApp.Common.Http
{
    public interface IBernieHttpClient
    {
        Task<IEnumerable<HitDataItem<NewsArticle>>> GetNewsAsync();

        Task<HitDataItem<NewsArticle>> GetNewsArticleAsync(string id);

        Task<IEnumerable<HitDataItem<Issue>>> GetIssuesAsync();
    }
}
