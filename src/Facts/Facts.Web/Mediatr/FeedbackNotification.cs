using Calabonga.UnitOfWork;
using Facts.Web.Mediatr.Base;
using Microsoft.Extensions.Logging;
using System;

namespace Facts.Web.Mediatr
{
    public class FeedbackNotification : NotificationBase
    {
        public FeedbackNotification(string content, Exception? exception = null)
    : base("FEEDBACK: jfacts.ru",  content, "noreply@jfacts.ru", "dev@calabonga.net", exception) {}
    }

    public class FeedbackNotificationHandler : NotificationHandlerBase<FeedbackNotification>
    {
        public FeedbackNotificationHandler(IUnitOfWork unitOfWork, ILogger<FeedbackNotification> logger) : base(unitOfWork, logger)
        {
        }
    }
}