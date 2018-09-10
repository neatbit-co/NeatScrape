using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NeatScrape.Scraping.Html;

namespace NeatScrape
{
    public class Scraper
    {
        private readonly ScraperConfiguration _configuration;

        public Scraper(Action<ScraperConfigurationBuilder> config = null)
        {
            var defaultConfig = new ScraperConfigurationBuilder();
            defaultConfig
                .UseScraper(new HtmlScraper(new StandardHtmlFetcher(maxConcurrentRequests: 10)));

            config?.Invoke(defaultConfig);
            _configuration = defaultConfig.build();
        }

        public async Task<ICollection<T>> Scrape<T>(HtmlScrapeInstruction<T> instruction) where T : IScrapeResult, new()
        {
            return await _configuration.Scraper.Scrape(instruction);
        }
    }
}
