using System;
using Fluency;

namespace NeatScrape
{
    public class ScrapeInstructionConfigurationBuilder<T> : FluentBuilder<ScrapeInstructionConfiguration<T>> where T: IScrapeResult, new()
    {
        public ScrapeInstructionConfigurationBuilder()
        {
            SetProperty(x => x.PagingUrlParameter, (string) null);
            SetProperty(x => x.PagingStart, 1);
            SetProperty(x => x.PagingIncrement, 1);
            SetProperty(x => x.PagingEnd, 1);
            SetProperty(x => x.MaxResults, (int?) null);
        }

        public ScrapeInstructionConfigurationBuilder<T> ScrapeEntries(Action<EntryScrapingConfigurationBuilder<T>> builderAction)
        {
            var defaultBuilder = new EntryScrapingConfigurationBuilder<T>();
            builderAction(defaultBuilder);
            SetProperty(x => x.EntriesConfiguration, defaultBuilder);
            return this;
        }

        public ScrapeInstructionConfigurationBuilder<T> WithPaging(string urlParameterName, int pagingStart, int pagingIncrement, int? pagingEnd = null, int? maxResults = null)
        {
            SetProperty(x => x.PagingUrlParameter, urlParameterName);
            SetProperty(x => x.PagingStart, pagingStart);
            SetProperty(x => x.PagingIncrement, pagingIncrement);
            SetProperty(x => x.PagingEnd, pagingEnd);
            SetProperty(x => x.MaxResults, maxResults);
            return this;
        }
    }
}