using System;
using HtmlAgilityPack;

namespace NeatScrape.Converters
{
    public class LinkConverter : NodeConverter<string>
    {
        private readonly Uri _baseUri;

        public LinkConverter(string baseUrl = null)
        {
            _baseUri = !string.IsNullOrEmpty(baseUrl) ? new Uri(baseUrl, UriKind.Absolute): null;
        }

        public override string Convert(HtmlNode node)
        {
            var href = node.GetAttributeValue("href", null);
            return _baseUri != null && !href.StartsWith("http")
                ? new Uri(_baseUri, href).ToString()
                : new Uri(href, UriKind.Absolute).ToString();
        }
    }
}
