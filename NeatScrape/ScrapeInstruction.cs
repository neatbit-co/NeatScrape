using NeatScrape.Scrapers.Html;
using System;
using System.Threading.Tasks;

namespace NeatScrape
{
    // TODO: Rename to HtmlScrapeInstruction
    public class ScrapeInstruction<T> where T : IScrapeResult, new()
    {
        private int _pagingCurrent = -1;

        public ScrapeInstruction(string url, Action<ScrapeInstructionConfigurationBuilder<T>> config)
        {
            Url = url;
            var defaultConfig = new ScrapeInstructionConfigurationBuilder<T>();
            config.Invoke(defaultConfig);
            Configuration = defaultConfig.build();

            // TODO: Validate config
        }

        /// <summary>
        /// URL to scrape
        /// </summary>
        public string Url { get; }

        public ScrapeInstructionConfiguration<T> Configuration { get; }

        /// <summary>
        /// Gets the next URL to be fetched. Returns false if no URL can be fetched.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public virtual bool TryGetNextUrl(out string url)
        {
            if (_pagingCurrent == -1)
            {
                _pagingCurrent = Configuration.PagingStart;
            }

            if (_pagingCurrent > Configuration.PagingEnd)
            {
                url = null;
                return false;
            }

            if (string.IsNullOrWhiteSpace(Configuration.PagingUrlParameter))
            {
                url = Url;
                _pagingCurrent++;
                return true;
            }

            url = Url.Replace("{{" + Configuration.PagingUrlParameter + "}}", _pagingCurrent.ToString());
            _pagingCurrent++;
            return true;
        }

        /// <summary>
        /// Returns next HTML content (page) or null if there is none.
        /// </summary>
        public virtual async Task<string> GetNextContent(IHtmlFetcher htmlFetcher) // TODO: Get IHtmlFetcher from Configuration
        {
            if (TryGetNextUrl(out var url))
            {
                return await htmlFetcher.FetchAsString(url);
            }

            return null;
        }
    }
}