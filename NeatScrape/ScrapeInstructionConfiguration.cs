using System.Collections.Generic;

namespace NeatScrape
{
    public class ScrapeInstructionConfiguration<T> where T: IScrapeResult, new()
    {
        /// <summary>
        /// Instructions for scraping entries
        /// </summary>
        public EntryScrapingConfiguration EntriesConfiguration { get; internal set; }

        /// <summary>
        /// URL parameter that defines paging, e.g. in "pages?page=5" the parameter would be "page".
        /// </summary>
        public string PagingUrlParameter { get; internal set; }

        /// <summary>
        /// Start value of the PagingUrlParameter
        /// </summary>
        public int PagingStart { get; internal set; }

        /// <summary>
        /// How much to increment PagingUrlParameter for each page
        /// </summary>
        public int PagingIncrement { get; internal set; }

        /// <summary>
        /// The last value of PagingUrlParameter to fetch
        /// If null pages are fetched until no new results are found
        /// </summary>
        public int? PagingEnd { get; internal set; }

        /// <summary>
        /// Maximum number of results to return
        /// If null pages are fetched until no new results are found.
        /// </summary>
        public int? MaxResults { get; internal set; }
    }
}