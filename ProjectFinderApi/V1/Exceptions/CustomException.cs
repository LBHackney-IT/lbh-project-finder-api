using System;

namespace ProjectFinderApi.V1.Exceptions
{
    public class PostUserException : Exception
    {
        public PostUserException(string message) : base(message) { }

    }

    public class PostProjectException : Exception
    {
        public PostProjectException(string message) : base(message) { }

    }
}
