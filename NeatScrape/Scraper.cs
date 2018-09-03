using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NeatScrape.Converters;
using NeatScrape.Scrapers;
using NeatScrape.Scrapers.Html;

namespace NeatScrape
{
    public class Scraper
    {
        private readonly IScraper _scraper;
        private readonly ScraperConfiguration _configuration;

        public Scraper(Action<ScraperConfigurationBuilder> config = null)
        {
            var defaultConfig = new ScraperConfigurationBuilder();
            defaultConfig
                .WithHtmlFetcher(new StandardHtmlFetcher(maxConcurrentRequests: 10));

            config?.Invoke(defaultConfig);
            _configuration = defaultConfig.build();

            _scraper = _configuration.Scraper ?? new HtmlScraper(_configuration.HtmlFetcher);

            //new ScrapeInstruction<Result>("url", config =>
            //    config
            //        .ScrapeEntries(e => e.FromXPath("entity")
            //            .ScrapeProperty(p => p.Title, p => p.FromXPath("title"))
            //            .ScrapeProperty(p => p.Price, p => p.FromXPath("price").UsingConverter(new PriceConverter())))
            //        .WithPaging("page", 1, 1)
            //);
        }

        public async Task<ICollection<T>> Scrape<T>(ScrapeInstruction<T> instruction) where T : IScrapeResult, new()
        {
            return await _scraper.Scrape(instruction);
        }
    }

    public class PriceConverter : IInnerTextConverter<decimal>
    {
        public decimal Convert(string innerText)
        {
            return 0.0m;
        }

        object IInnerTextConverter.Convert(string innerText)
        {
            return Convert(innerText);
        }
    }

    public class Result : IScrapeResult
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
    }
}
