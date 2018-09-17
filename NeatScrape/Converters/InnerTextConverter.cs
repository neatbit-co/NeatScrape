using HtmlAgilityPack;

namespace NeatScrape.Converters
{
    public class InnerTextConverter : NodeConverter<string>
    {
        public override string Convert(HtmlNode node)
        {
            return node.InnerText;
        }
    }
}