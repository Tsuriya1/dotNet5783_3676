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
    [Serializable]
    public class DalConfigException : Exception
    {
        public DalConfigException(string msg) : base(msg) { }
        public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
    }


}

