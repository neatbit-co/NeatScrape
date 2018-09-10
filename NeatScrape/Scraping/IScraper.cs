using System.Collections.Generic;
using System.Threading.Tasks;
using NeatScrape.Scraping.Html;

namespace NeatScrape.Scraping
{
    public interface IScraper
    {
        Task<ICollection<T>> Scrape<T>(HtmlScrapeInstruction<T> instruction) where T : IScrapeResult, new();
    }
}
