using System;

namespace NeatScrape
{
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
        public bool TryGetNextUrl(out string url)
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
    }
}