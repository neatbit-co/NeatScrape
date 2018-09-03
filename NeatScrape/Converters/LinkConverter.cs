using System;
using HtmlAgilityPack;

namespace NeatScrape.Converters
{
    public class LinkConverter : INodeConverter<string>
    {
        private readonly Uri _baseUri;

        public LinkConverter(string baseUrl = null)
        {
            _baseUri = baseUrl != null ? new Uri(baseUrl, UriKind.Absolute): null;
        }

        public string Convert(HtmlNode node)
        {
            var href = node.GetAttributeValue("href", null);
            return _baseUri != null && !href.StartsWith("http")
                ? new Uri(_baseUri, href).ToString()
                : new Uri(href, UriKind.Absolute).ToString();
        }

        object INodeConverter.Convert(HtmlNode node)
        {
            return Convert(node);
        }
    }
}
