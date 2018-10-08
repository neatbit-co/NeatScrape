using System;

namespace NeatScrape.Scraping.Html
{
    public class HtmlScrapeInstruction<T>: IScrapeInstruction where T : IScrapeResult, new()
    {
        public HtmlScrapeInstruction(Action<HtmlScrapeInstructionConfigurationBuilder<T>> config)
        {
            var defaultConfig = new HtmlScrapeInstructionConfigurationBuilder<T>();
            config.Invoke(defaultConfig);
            Configuration = defaultConfig.build();

            // TODO: Validate config
        }

        public HtmlScrapeInstructionConfiguration Configuration { get; }

        internal HtmlScrapingSession StartScrapingSession(IHtmlFetcher htmlFetcher)
        {
            return new HtmlScrapingSession(Configuration, htmlFetcher);
        }
    }
}