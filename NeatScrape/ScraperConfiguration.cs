using NeatScrape.Scrapers;
using NeatScrape.Scrapers.Html;

namespace NeatScrape
{
    public class ScraperConfiguration
    {
        public IScraper Scraper { get; internal set; }
        public IHtmlFetcher HtmlFetcher { get; internal set; }
    }
}