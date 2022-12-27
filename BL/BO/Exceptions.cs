using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class NotFoundError : Exception
    {
        public NotFoundError() { }

        public NotFoundError(string message)
            : base(message) { }

        public NotFoundError(string message, Exception inner)
            : base(message, inner) { }
    }
    public class StockError : Exception
    {
        public StockError() { }
        public StockError(string massage)
            : base(massage) { }
        public StockError (string massage, Exception inner)
            : base(massage, inner) { }
    }
    public class NotValidValue : Exception
    {
        public NotValidValue() { }
        public NotValidValue(string massage)
            : base(massage) { }
        public NotValidValue(string massage, Exception inner)
            : base(massage, inner) { }
    }
    public class StatusError : Exception
    {
        public StatusError() { }
        public StatusError(string massage)
            : base(massage) { }
        public StatusError(string massage, Exception inner)
            : base(massage, inner) { }
    }
}

