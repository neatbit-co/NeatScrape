namespace NeatScrape.Converters
{
    public interface IPropertyValueConverter
    {
        /// <summary>
        /// Converts a property value from a scraper to another type of object
        /// </summary>
        object Convert(object value);
    }

    public interface IPropertyValueConverter<out T> : IPropertyValueConverter
    {
        /// <summary>
        /// Converts a property value from a scraper to an object of type T
        /// </summary>
        new T Convert(object value);
    }
}
