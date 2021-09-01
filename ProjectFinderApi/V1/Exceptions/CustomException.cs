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

    public class GetProjectsException : Exception
    {
        public GetProjectsException(string message) : base(message) { }
    }

    public class PostProjectMemberException : Exception
    {
        public PostProjectMemberException(string message) : base(message) { }

    }

    public class GetProjectMembersException : Exception
    {
        public GetProjectMembersException(string message) : base(message) { }

    }

    public class PostProjectLinkException : Exception
    {
        public PostProjectLinkException(string message) : base(message) { }

    }

    public class GetProjectLinksException : Exception
    {
        public GetProjectLinksException(string message) : base(message) { }

    }
}
