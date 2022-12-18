using System;
namespace DalFacade.DO
{
    public class DuplicateException : Exception
    {
        public DuplicateException() { }

        public DuplicateException(string message)
            : base(message) { }

        
    }

    public class NotFoundException : Exception
    {
        public NotFoundException() { }

        public NotFoundException(string message)
            : base(message) { }

        
    }
}

