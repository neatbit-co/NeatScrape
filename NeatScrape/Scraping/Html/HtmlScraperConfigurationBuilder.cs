using Fluency;
using NeatScrape.Converters;

namespace NeatScrape.Scraping.Html
{
    public class HtmlScraperConfigurationBuilder : FluentBuilder<HtmlScraperConfiguration>
    {
        public HtmlScraperConfigurationBuilder()
        {
            // Set defaults
            UseHtmlFetcher(new StandardHtmlFetcher(maxConcurrentRequests: 10));
            UseDefaultNodeConverter(new TextNodeConverter());
        }

        public HtmlScraperConfigurationBuilder UseHtmlFetcher(IHtmlFetcher htmlFetcher)
        {
            SetProperty(x => x.HtmlFetcher, htmlFetcher);
            return this;
        }

        public HtmlScraperConfigurationBuilder UseDefaultNodeConverter(INodeConverter nodeConverter)
        {
            SetProperty(x => x.DefaultNodeConverter, nodeConverter);
            return this;
        }
    }
}