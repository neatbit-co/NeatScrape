using System;
using System.Runtime.Serialization;

namespace NeatScrape.Exceptions
{
    public class PropertyValueConversionException : Exception
    {
        public PropertyValueConversionException()
        {
        }

        protected PropertyValueConversionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public PropertyValueConversionException(string message) : base(message)
        {
        }

        public PropertyValueConversionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}