namespace NeatScrape.Tests.Scenarios.GivenHtmlScraper
{
    public abstract class GivenHtmlScraper : Scenario
    {
        public Scraper Scraper { get; private set; }

        public override void Given()
        {
            Scraper = new Scraper(c =>
            {
                c.UseScraper(new Scraping.Html.HtmlScraper(new ResourceHtmlFetcher()));
            });
        }
    }
}
