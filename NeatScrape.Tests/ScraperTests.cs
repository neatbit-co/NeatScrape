using System.Threading.Tasks;
using NeatScrape.Converters;
using Xunit;

namespace NeatScrape.Tests
{
    public class ScraperTests
    {
        [Fact]
        public async Task ScrapingSiteShouldReturnResults()
        {
            var scraper = new Scraper();
            var instruction = new ScrapeInstruction<CameraItemEntry>("https://foorumi.kameralaukku.com/forums/myydaeaen.9/page-{{page}}", config =>
            {
                var linkConverter = new LinkConverter("https://foorumi.kameralaukku.com/forums/");

                config.ScrapeEntries(e => e
                    .FromCssSelector(".discussionListItem")
                    .ScrapeProperty(p => p.Title, p => p.FromCssSelector(".title a:nth-child(2)"))
                    .ScrapeProperty(p => p.Url, p => p.FromCssSelector(".title a:nth-child(2)").UsingConverter(linkConverter)))
                .WithPaging("page", pagingStart: 1, pagingIncrement: 1, pagingEnd: 5);
            });

            var results = await scraper.Scrape(instruction);
            Assert.NotEmpty(results);
        }
    }

    public class CameraItemEntry : IScrapeResult
    {
        public string Title { get; set; }
        public string Url { get; set; }
    }
}
