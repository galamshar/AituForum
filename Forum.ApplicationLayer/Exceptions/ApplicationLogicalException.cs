using System;

namespace Forum.ApplicationLayer.Exceptions
{
    public class ApplicationLogicalException : Exception
    {
        public ApplicationLogicalException()
        {
        }

        public ApplicationLogicalException(string message) : base(message)
        {
        }

        public ApplicationLogicalException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
