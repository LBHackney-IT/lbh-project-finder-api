using System;

namespace ProjectFinderApi.V1.Exceptions
{
    public class PostUserException : Exception
    {
        public PostUserException(string message) : base(message) { }
    }
}
