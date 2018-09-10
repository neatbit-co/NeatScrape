using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Fluency;
using NeatScrape.Utils;

namespace NeatScrape.Scraping.Html
{
    public class EntryScrapingConfigurationBuilder<T> : FluentBuilder<EntryScrapingConfiguration> where T: IScrapeResult, new()
    {
        public EntryScrapingConfigurationBuilder<T> FromXPath(string xPath)
        {
            SetProperty(x => x.Selector, new QuerySelector(SelectorKind.XPath, xPath));
            SetProperty(x => x.PropertyConfigurations, new List<PropertyScrapingConfiguration>());
            return this;
        }

        public EntryScrapingConfigurationBuilder<T> FromCssSelector(string cssSelector)
        {
            SetProperty(x => x.Selector, new QuerySelector(SelectorKind.CssSelector, cssSelector));
            return this;
        }

        public EntryScrapingConfigurationBuilder<T> MapProperty<TProp>(Expression<Func<T, TProp>> propertyExpression, Action<PropertyScrapingConfigurationBuilder<TProp>> builderAction)
        {
            var defaultBuilder = new PropertyScrapingConfigurationBuilder<TProp>(propertyExpression.GetName());
            builderAction(defaultBuilder);
            AddToList(x => x.PropertyConfigurations, defaultBuilder);
            return this;
        }
    }
}