using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeatScrape.Scraping.Html
{
    public interface IHtmlScraper : IScraper
    {
        Task<ICollection<T>> Scrape<T>(HtmlScrapeInstruction<T> instruction) where T : IScrapeResult, new();
    }
}