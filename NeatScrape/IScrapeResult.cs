namespace NeatScrape
{
    public interface IScrapeResult
    {
        /// <summary>
        /// A unique key that identifies a result
        /// </summary>
        string Key { get; }
    }
}
