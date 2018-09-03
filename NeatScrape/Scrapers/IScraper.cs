using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeatScrape.Scrapers
{
    public interface IScraper
    {
        Task<ICollection<T>> Scrape<T>(ScrapeInstruction<T> instruction) where T : IScrapeResult, new();
    }
}
