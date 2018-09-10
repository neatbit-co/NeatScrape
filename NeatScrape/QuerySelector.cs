using NeatScrape.Scraping.Html;

namespace NeatScrape
{
    public class QuerySelector
    {
        public QuerySelector(SelectorKind selectorKind, string selector)
        {
            SelectorKind = selectorKind;
            Selector = selector;
        }

        public SelectorKind SelectorKind { get; }
        public string Selector { get; }
    }
}