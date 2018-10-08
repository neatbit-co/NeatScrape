using System.Collections.Generic;
using FluentAssertions;
using NeatScrape.Converters;
using NeatScrape.Scraping.Html;
using Xunit;

namespace NeatScrape.Tests.Scenarios.GivenThreePagesOfProducts
{
    public class WhenPagesFrom1To3AreScraped : GivenThreePagesOfProducts
    {
        private ICollection<AmazonEntry> _results;

        public override void When()
        {
            IHtmlScraper scraper = new HtmlScraper(c =>
            {
                c.UseHtmlFetcher(new ResourceHtmlFetcher());
            });

            var instruction = new HtmlScrapeInstruction<AmazonEntry>(config =>
            {
                var linkConverter = new LinkConverter("https://www.amazon.com");

                config
                    .ScrapeUrl(Resource)
                    .WithPaging("page", pagingStart: 1, pagingIncrement: 1, pagingEnd: 3)
                    .AsEntries(e => e.FromCssSelector(".s-item-container")
                        .MapProperty(p => p.Title, p => p.FromCssSelector("a.s-access-detail-page h2"))
                        .MapProperty(p => p.Url, p => p.FromCssSelector("a.s-access-detail-page").UsingConverter(linkConverter)));
            });

            _results = scraper.Scrape(instruction).Result;
        }

        public class Then : IClassFixture<WhenPagesFrom1To3AreScraped>
        {
            private readonly WhenPagesFrom1To3AreScraped _data;

            public Then(WhenPagesFrom1To3AreScraped data)
            {
                _data = data;
            }

            [Fact]
            public void Then90ResultsAreReturned()
            {
                _data._results.Should().HaveCount(90);
            }

            [Fact]
            public void ThenAllResultsShouldHaveTitle()
            {
                _data._results.Should().OnlyContain(x => !string.IsNullOrWhiteSpace(x.Title));
            }

            [Fact]
            public void ThenAllResultsShouldHaveUrl()
            {
                _data._results.Should().OnlyContain(x => !string.IsNullOrWhiteSpace(x.Url));
            }
        }
    }
}
