using HtmlAgilityPack;

namespace NeatScrape.Converters
{
    public interface INodeConverter : IPropertyValueConverter
    {
        object Convert(HtmlNode node);
    }

    public interface INodeConverter<T> : INodeConverter, IPropertyValueConverter<T>
    {
        new T Convert(HtmlNode node);
    }
}