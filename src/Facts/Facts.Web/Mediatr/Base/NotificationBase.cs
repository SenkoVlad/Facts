using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Facts.Web.Mediatr.Base
{
    public abstract class NotificationBase : INotification
    {
        public string Subject { get; }
        public string Content { get;  }
        public string AddressFrom { get;  }
        public string AddressTo { get;  }
        public Exception? Exception { get; }

        protected NotificationBase(string subject, string content, string addressFrom, string addressTo, Exception? exception = null)
        {
            Subject = subject;
            Content = content;
            AddressFrom = addressFrom;
            AddressTo = addressTo;
            Exception = exception;
        }
    }
}
