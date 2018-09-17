namespace NeatScrape.Tests.Scenarios.GivenThreePagesOfProducts
{
    public abstract class GivenThreePagesOfProducts : Scenario
    {
        public string Resource => "NeatScrape.Tests.Resources.amazon_pencils_page{{page}}.html";

        public override void Given()
        {
            // Nothing to do here
        }
    }
}
