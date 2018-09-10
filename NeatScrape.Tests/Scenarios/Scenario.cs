namespace NeatScrape.Tests.Scenarios
{
    public abstract class Scenario
    {
        protected Scenario()
        {
            Given();
            When();
        }

        public abstract void Given();
        public abstract void When();
    }
}
