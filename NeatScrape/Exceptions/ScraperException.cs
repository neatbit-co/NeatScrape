using System;
using System.Runtime.Serialization;

namespace NeatScrape.Exceptions
{
    public class ScraperException : Exception
    {
        public ScraperException()
        {
        }

        protected ScraperException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ScraperException(string message) : base(message)
        {
        }

        public ScraperException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
