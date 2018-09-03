using System.Threading.Tasks;

namespace NeatScrape.Scrapers.Html
{
    public interface IHtmlFetcher
    {
        Task<string> FetchAsString(string url);
    }
}
