using Fluency;
using NeatScrape.Scraping;

namespace NeatScrape
{
    public class ScraperConfigurationBuilder : FluentBuilder<ScraperConfiguration>
    {
        public ScraperConfigurationBuilder UseScraper(IScraper scraper)
        {
            SetProperty(x => x.Scraper, scraper);
            return this;
        }
    }
}