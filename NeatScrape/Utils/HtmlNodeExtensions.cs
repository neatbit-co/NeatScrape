using System.Collections.Generic;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using NeatScrape.Exceptions;
using NeatScrape.Scraping.Html;

namespace NeatScrape.Utils
{
    internal static class HtmlNodeExtensions
    {
        public static IList<HtmlNode> QueryAll(this HtmlNode root, QuerySelector query)
        {
            switch (query.SelectorKind)
            {
                case SelectorKind.XPath:
                    return root.SelectNodes(query.Selector);

                case SelectorKind.CssSelector:
                    return root.QuerySelectorAll(query.Selector);

                default:
                    throw new UnsupportedSelectorException($"Query selector kind {query.SelectorKind} is not supported.");
            }
        }

        public static HtmlNode QuerySingle(this HtmlNode root, QuerySelector query)
        {
            switch (query.SelectorKind)
            {
                case SelectorKind.XPath:
                    return root.SelectSingleNode(query.Selector);

                case SelectorKind.CssSelector:
                    return root.QuerySelector(query.Selector);

                default:
                    throw new UnsupportedSelectorException($"Query selector kind {query.SelectorKind} is not supported.");
            }
        }
    }
}
