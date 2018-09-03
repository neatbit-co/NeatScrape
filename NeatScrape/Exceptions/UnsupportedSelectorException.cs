using System;
using System.Runtime.Serialization;

namespace NeatScrape.Exceptions
{
    public class UnsupportedSelectorException : Exception
    {
        public UnsupportedSelectorException()
        {
        }

        protected UnsupportedSelectorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public UnsupportedSelectorException(string message) : base(message)
        {
        }

        public UnsupportedSelectorException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
