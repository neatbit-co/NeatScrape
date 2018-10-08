using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeatScrape
{
    public interface IScraper
    {
        Task<ICollection<T>> Scrape<T>(IScrapeInstruction instruction) where T : IScrapeResult, new();
    }
}