using System;
using System.Threading.Tasks;

namespace NeatScrape.Scraping.Html
{
    internal class HtmlScrapingSession
    {
        private readonly HtmlScrapeInstructionConfiguration _config;
        private readonly IHtmlFetcher _htmlFetcher;

        private int _pagingCurrent = -1;

        public HtmlScrapingSession(HtmlScrapeInstructionConfiguration config, IHtmlFetcher htmlFetcher)
        {
            _config = config;
            _htmlFetcher = htmlFetcher;
        }

        /// <summary>
        /// Gets the next URL to be fetched. Returns false if no URL can be fetched.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public virtual bool TryGetNextUrl(out string url)
        {
            if (_pagingCurrent == -1)
            {
                _pagingCurrent = _config.PagingStart;
            }

            if (_pagingCurrent > _config.PagingEnd)
            {
                url = null;
                return false;
            }

            if (string.IsNullOrWhiteSpace(_config.PagingUrlParameter))
            {
                url = _config.Url;
                _pagingCurrent++;
                return true;
            }

            url = _config.Url.Replace("{{" + _config.PagingUrlParameter + "}}", _pagingCurrent.ToString());
            _pagingCurrent++;
            return true;
        }

        /// <summary>
        /// Returns next HTML content (page) or null if there is none.
        /// </summary>
        public virtual async Task<string> GetNextContent()
        {
            if (TryGetNextUrl(out var url))
            {
                try
                {
                    return await _htmlFetcher.FetchAsString(url);
                }
                catch (Exception)
                {
                    // TODO: Add logging
                    return null;
                }
            }

            return null;
        }
    }
}