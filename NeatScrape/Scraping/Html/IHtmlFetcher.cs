using System.Threading.Tasks;

namespace NeatScrape.Scraping.Html
{
    public interface IHtmlFetcher
    {
        Task<string> FetchAsString(string url);
    }
}
