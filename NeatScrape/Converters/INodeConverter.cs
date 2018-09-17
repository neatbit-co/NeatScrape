using HtmlAgilityPack;

namespace NeatScrape.Converters
{
    public interface INodeConverter : IPropertyValueConverter
    {
        /// <summary>
        /// Converts an HTML node to an object
        /// </summary>
        object Convert(HtmlNode node);
    }
}