using Fluency;
using NeatScrape.Scrapers;
using NeatScrape.Scrapers.Html;

namespace NeatScrape
{
    public class ScraperConfigurationBuilder : FluentBuilder<ScraperConfiguration>
    {
        public ScraperConfigurationBuilder WithScraper(IScraper scraper)
        {
            SetProperty(x => x.Scraper, scraper);
            return this;
        }

        public ScraperConfigurationBuilder WithHtmlFetcher(IHtmlFetcher htmlFetcher)
        {
            SetProperty(x => x.HtmlFetcher, htmlFetcher);
            return this;
        }
    }
}