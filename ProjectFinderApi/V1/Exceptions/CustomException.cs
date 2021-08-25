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

    public class PatchProjectException : Exception
    {
        public PatchProjectException(string message) : base(message) { }

    }

    public class DeleteProjectException : Exception
    {
        public DeleteProjectException(string message) : base(message) { }

    }
}
