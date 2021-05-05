using System;

namespace Application.Common.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException() : base("Bad request")
        {
            
        }

        public BadRequestException(string message) : base(message)
        {
            
        }
    }
}
