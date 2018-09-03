using System.Collections.Generic;

namespace NeatScrape
{
    public class EntryScrapingConfiguration
    {
        /// <summary>
        /// Selector expression that returns individual entries for further parsing by PropertyConfigurations
        /// </summary>
        public QuerySelector Selector { get; internal set; }

        /// <summary>
        /// Instructions for scraping values for each property in result class
        /// </summary>
        public IList<PropertyScrapingConfiguration> PropertyConfigurations { get; internal set; }
    }
}