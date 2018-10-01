using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fluency.Utils;
using HtmlAgilityPack;
using NeatScrape.Converters;
using NeatScrape.Exceptions;
using NeatScrape.Utils;

namespace NeatScrape.Scraping.Html
{
    public class HtmlScraper : IScraper
    {
        private readonly IHtmlFetcher _htmlFetcher;
        private readonly INodeConverter _defaultNodeConverter;

        public HtmlScraper(IHtmlFetcher htmlFetcher, INodeConverter defaultNodeConverter = null)
        {
            _htmlFetcher = htmlFetcher;
            _defaultNodeConverter = defaultNodeConverter ?? new TextNodeConverter();
        }

        public async Task<ICollection<T>> Scrape<T>(HtmlScrapeInstruction<T> instruction) where T : IScrapeResult, new()
        {
            var resultsByKey = new Dictionary<string, T>();
            var session = instruction.StartScrapingSession(_htmlFetcher);

            do
            {
                var html = await session.GetNextContent();
                if (html == null)
                {
                    break;
                }

                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                var entryNodes = GetEntryNodes(instruction, doc).ToList();
                if (!entryNodes.Any())
                {
                    break;
                }

                bool foundNewResults = false;
                foreach (var entryNode in entryNodes)
                {
                    if (entryNode == null)
                    {
                        // TODO: Log
                        continue;
                    }

                    if (TryParseNode(entryNode, instruction, out var result) && !resultsByKey.ContainsKey(result.Key))
                    {
                        resultsByKey[result.Key] = result;
                        foundNewResults = true;
                    }
                }

                if (!foundNewResults)
                {
                    break;
                }

            } while (true);

            return resultsByKey.Values;
        }

        private bool TryParseNode<T>(HtmlNode entryNode, HtmlScrapeInstruction<T> instruction, out T result)
            where T : IScrapeResult, new()
        {
            result = new T();
            var hasProperties = false;

            foreach (var property in instruction.Configuration.EntriesConfiguration.PropertyConfigurations.Where(x => x.Selector?.Selector != null))
            {
                var node = entryNode.QuerySingle(property.Selector);
                if (node != null)
                {
                    var value = (property.Converter ?? _defaultNodeConverter).Convert(node);
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

        private static IEnumerable<HtmlNode> GetEntryNodes<T>(HtmlScrapeInstruction<T> instruction, HtmlDocument doc)
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
    }
}