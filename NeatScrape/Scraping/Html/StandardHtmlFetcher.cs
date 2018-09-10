using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace NeatScrape.Scraping.Html
{
    public class StandardHtmlFetcher : IHtmlFetcher
    {
        private readonly SemaphoreSlim _batch;
        private readonly HttpClient _httpClient;

        public StandardHtmlFetcher(int maxConcurrentRequests)
        {
            _httpClient = new HttpClient();
            _batch = new SemaphoreSlim(maxConcurrentRequests, maxConcurrentRequests);
        }

        public async Task<string> FetchAsString(string url)
        {
            await _batch.WaitAsync();

            try
            {
                using (var response = await _httpClient.GetAsync(url))
                {
                    using (var content = response.Content)
                    {
                        return await content.ReadAsStringAsync();
                    }
                }
            }
            finally
            {
                _batch.Release();
            }
        }
    }
}