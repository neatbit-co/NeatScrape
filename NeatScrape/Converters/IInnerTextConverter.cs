namespace NeatScrape.Converters
{
    public interface IInnerTextConverter : IPropertyValueConverter
    {
        object Convert(string innerText);
    }

    public interface IInnerTextConverter<T> : IInnerTextConverter, IPropertyValueConverter<T>
    {
        new T Convert(string innerText);
    }
}