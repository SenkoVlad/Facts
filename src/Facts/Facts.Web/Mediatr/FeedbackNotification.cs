using Calabonga.Facts.Web.ViewModels;
using Calabonga.UnitOfWork;
using Facts.Web.Mediatr.Base;
using Microsoft.Extensions.Logging;
using System;

namespace Facts.Web.Mediatr
{
    public class FeedbackNotification : NotificationBase
    {
        public FeedbackNotification(FeedbackViewModel model)
            : base("FEEDBACK: jfacts.ru",  model.ToString(), "noreply@jfacts.ru", "dev@calabonga.net", null) {}

    }
    public class FeedbackNotificationHandler : NotificationHandlerBase<FeedbackNotification>
    {
        public FeedbackNotificationHandler(IUnitOfWork unitOfWork, ILogger<FeedbackNotification> logger) : base(unitOfWork, logger)
        { 
        }
    }
}