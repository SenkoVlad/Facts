using Calabonga.UnitOfWork;
using Facts.Web.Mediatr.Base;
using Microsoft.Extensions.Logging;
using System;

namespace Facts.Web.Mediatr
{
    public class ErrorNotification : NotificationBase
    {
        public ErrorNotification( string content, Exception? exception = null)
             : base("ERROR: jfacts.ru", content, "noreply@jfacts.ru", "dev@calabonga.net", exception) { }       
    }

    public class ErrorNotificationHandler : NotificationHandlerBase<ErrorNotification>
    {
        public ErrorNotificationHandler(IUnitOfWork unitOfWork, ILogger<ErrorNotification> logger) : base(unitOfWork, logger)
        {
        }
    }
}