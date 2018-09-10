using HtmlAgilityPack;

namespace NeatScrape.Converters
{
    public class InnerTextConverter : INodeConverter<string>
    {
        public string Convert(HtmlNode node)
        {
            return node.InnerText;
        }

        object INodeConverter.Convert(HtmlNode node)
        {
            return Convert(node);
        }
    }
}