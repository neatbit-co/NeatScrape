# What is NeatScrape

NeatScrape is a C# library to scrape HTML content from web pages and turn it into structured data that can be consumed by your .NET Core application.

# Installation (Nuget)

**Warning:** This library is still in early alpha, is likely to get breaking changes often and is not ready for production use.

```
Install-Package NeatScrape -IncludePrerelease
```

# Usage

```csharp
// Define the shape of results that you want the Scraper to extract
public class AmazonEntry : IScrapeResult
{
    // Include any properties you want (string, int etc.)
    public string Title { get; set; }
    public string Url { get; set; }
    
    // A key identifies each result in a unique way.
    // Since we only expect one result per URL we can use this as a key.
    public string Key => Url;
}
```

```csharp
// Initialize a new Html Scraper instance
IHtmlScraper scraper = new HtmlScraper();

// Define instructions on how to fetch and parse the AmazonEntry results from a web page
var instruction = new HtmlScrapeInstruction<AmazonEntry>(config =>
{
    // Define a link converter with a base URL to be used for parsing the URL property
    var linkConverter = new LinkConverter("https://www.amazon.com");

    // Configure URL, paging and how to scrape/map the properties
    config
        .ScrapeUrl("https://www.amazon.com/s/?keywords=pencils&page={{page}}")
        .WithPaging("page", pagingStart: 1, pagingIncrement: 1, pagingEnd: 3)
        .AsEntries(e => e.FromCssSelector(".s-item-container")
            .MapProperty(
                p => p.Title,
                p => p.FromCssSelector("a.s-access-detail-page h2"))
            .MapProperty(
                p => p.Url,
                p => p.FromCssSelector("a.s-access-detail-page")
                        .UsingConverter(linkConverter)));
});

// Call scraper to do the actual scraping
ICollection<AmazonEntry> results = await scraper.Scrape(instruction);
```

See tests for more examples.

# License

MIT