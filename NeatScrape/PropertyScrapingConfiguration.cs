using NeatScrape.Converters;

namespace NeatScrape
{
    public class PropertyScrapingConfiguration
    {
        public string PropertyName { get; internal set; }
        public QuerySelector Selector { get; internal set; }
        public IPropertyValueConverter Converter { get; internal set; }
    }
}