using Fluency;
using NeatScrape.Converters;

namespace NeatScrape.Scraping.Html
{
    public class PropertyScrapingConfigurationBuilder<TProp> : FluentBuilder<PropertyScrapingConfiguration>
    {
        public PropertyScrapingConfigurationBuilder(string propertyName)
        {
            SetProperty(x => x.PropertyName, propertyName);
            SetProperty(x => x.Converter, (IPropertyValueConverter) null);
            SetProperty(x => x.Selector, (QuerySelector) null);
        }

        public PropertyScrapingConfigurationBuilder<TProp> UsingConverter(IPropertyValueConverter<TProp> converter)
        {
            SetProperty(x => x.Converter, converter);
            return this;
        }

        public PropertyScrapingConfigurationBuilder<TProp> FromXPath(string xPath)
        {
            SetProperty(x => x.Selector, new QuerySelector(SelectorKind.XPath, xPath));
            return this;
        }

        public PropertyScrapingConfigurationBuilder<TProp> FromCssSelector(string cssSelector)
        {
            SetProperty(x => x.Selector, new QuerySelector(SelectorKind.CssSelector, cssSelector));
            return this;
        }
    }
}