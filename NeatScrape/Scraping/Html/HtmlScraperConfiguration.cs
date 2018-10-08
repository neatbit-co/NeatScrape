using NeatScrape.Converters;

namespace NeatScrape.Scraping.Html
{
    public class HtmlScraperConfiguration
    {
        public IHtmlFetcher HtmlFetcher { get; internal set; }
        public INodeConverter DefaultNodeConverter { get; internal set; }
    }
}