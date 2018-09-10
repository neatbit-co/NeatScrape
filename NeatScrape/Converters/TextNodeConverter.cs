using HtmlAgilityPack;

namespace NeatScrape.Converters
{
    public class TextNodeConverter : INodeConverter<string>
    {
        public string Convert(HtmlNode node)
        {
            return node.SelectSingleNode("./text()").InnerText;
        }

        object INodeConverter.Convert(HtmlNode node)
        {
            return Convert(node);
        }
    }
}
