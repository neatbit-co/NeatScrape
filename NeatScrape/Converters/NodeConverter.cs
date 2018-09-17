using HtmlAgilityPack;

namespace NeatScrape.Converters
{
    public abstract class NodeConverter<T> : INodeConverter, IPropertyValueConverter<T>
    {
        /// <summary>
        /// Converts an HTML node to an object of type T
        /// </summary>
        public abstract T Convert(HtmlNode node);

        public virtual T Convert(object value)
        {
            return Convert(value as HtmlNode);
        }

        object IPropertyValueConverter.Convert(object value)
        {
            return Convert(value);
        }

        object INodeConverter.Convert(HtmlNode node)
        {
            return Convert(node);
        }
    }
}