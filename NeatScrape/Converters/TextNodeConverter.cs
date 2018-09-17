using HtmlAgilityPack;

namespace NeatScrape.Converters
{
    public class TextNodeConverter : NodeConverter<string>
    {
        public override string Convert(HtmlNode node)
        {
            return node.SelectSingleNode("./text()").InnerText;
        }
    }
}
