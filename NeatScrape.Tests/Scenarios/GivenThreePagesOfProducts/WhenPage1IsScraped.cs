using System.Collections.Generic;
using FluentAssertions;
using NeatScrape.Converters;
using NeatScrape.Scraping.Html;
using Xunit;

namespace NeatScrape.Tests.Scenarios.GivenThreePagesOfProducts
{
    public class WhenPage1IsScraped : GivenThreePagesOfProducts
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
                    .WithPaging("page", pagingStart: 1, pagingIncrement: 1, pagingEnd: 1)
                    .AsEntries(e => e.FromCssSelector(".s-item-container")
                        .MapProperty(p => p.Title, p => p.FromCssSelector("a.s-access-detail-page h2"))
                        .MapProperty(p => p.Url, p => p.FromCssSelector("a.s-access-detail-page").UsingConverter(linkConverter)));
            });

            _results = scraper.Scrape(instruction).Result;
        }

        public class Then : IClassFixture<WhenPage1IsScraped>
        {
            private readonly WhenPage1IsScraped _data;

            public Then(WhenPage1IsScraped data)
            {
                _data = data;
            }

            [Fact]
            public void Then30ResultsAreReturned()
            {
                _data._results.Should().HaveCount(30);
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
