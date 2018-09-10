using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NeatScrape.Scraping.Html;

namespace NeatScrape.Tests
{
    public class ResourceHtmlFetcher : IHtmlFetcher
    {
        private readonly Assembly _assembly;

        public ResourceHtmlFetcher()
        {
            _assembly = Assembly.GetExecutingAssembly();
        }

        public async Task<string> FetchAsString(string url)
        {
            var resourceStream = _assembly.GetManifestResourceStream(url);
            using (var reader = new StreamReader(resourceStream, Encoding.UTF8))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}