using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fluency.Utils;
using HtmlAgilityPack;
using NeatScrape.Converters;
using NeatScrape.Exceptions;
using NeatScrape.Utils;

namespace NeatScrape.Scrapers.Html
{
    public class HtmlScraper : IScraper
    {
        private readonly IHtmlFetcher _htmlFetcher;

        public HtmlScraper(IHtmlFetcher htmlFetcher)
        {
            _htmlFetcher = htmlFetcher;
        }

        public async Task<ICollection<T>> Scrape<T>(ScrapeInstruction<T> instruction) where T : IScrapeResult, new()
        {
            var results = new List<T>();

            do
            {
                var html = await instruction.GetNextContent(_htmlFetcher);
                if (html == null)
                {
                    break;
                }

                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                var entryNodes = GetEntryNodes(instruction, doc);

                foreach (var entryNode in entryNodes)
                {
                    if (entryNode == null)
                    {
                        // TODO: Log
                        continue;
                    }

                    if (TryParseNode(entryNode, instruction, out var result))
                    {
                        results.Add(result);
                    }
                }
            } while (true);

            return results;
        }

        private static bool TryParseNode<T>(HtmlNode entryNode, ScrapeInstruction<T> instruction, out T result)
            where T : IScrapeResult, new()
        {
            result = new T();
            var hasProperties = false;

            foreach (var property in instruction.Configuration.EntriesConfiguration.PropertyConfigurations)
            {
                var node = entryNode.QuerySingle(property.Selector);
                if (node != null)
                {
                    object value = property.Converter != null
                        ? Convert(property.Converter, node)
                        : node.InnerText;

                    result.SetProperty(property.PropertyName, value);
                    hasProperties = true;
                }
                else
                {
                    // TODO: Log
                }
            }

            return hasProperties;
        }

        private static IEnumerable<HtmlNode> GetEntryNodes<T>(ScrapeInstruction<T> instruction, HtmlDocument doc)
            where T : IScrapeResult, new()
        {
            var entryNodes = doc.DocumentNode.QueryAll(instruction.Configuration.EntriesConfiguration.Selector);
            if (entryNodes == null || !entryNodes.Any())
            {
                // TODO: Log
                throw new ScraperException(
                    $"Query '{instruction.Configuration.EntriesConfiguration.Selector.Selector}' didn't return any results");
            }

            return entryNodes;
        }

        private static object Convert(IPropertyValueConverter converter, HtmlNode node)
        {
            switch (converter)
            {
                case IInnerTextConverter innerTextConverter:
                    return innerTextConverter.Convert(node.InnerText);

                case INodeConverter nodeConverter:
                    return nodeConverter.Convert(node);

                default:
                    throw new PropertyValueConversionException(
                        $"There is no support for {converter.GetType()}. Please implement {nameof(IInnerTextConverter)} or {nameof(INodeConverter)}.");
            }
        }
    }
}