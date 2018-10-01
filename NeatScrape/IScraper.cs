using System.Collections.Generic;
using System.Threading.Tasks;
using NeatScrape.Scraping.Html;

namespace NeatScrape
{
    public interface IScraper
    {
        Task<ICollection<T>> Scrape<T>(HtmlScrapeInstruction<T> instruction) where T : IScrapeResult, new();
    }
}