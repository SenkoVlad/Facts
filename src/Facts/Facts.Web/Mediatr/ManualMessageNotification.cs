using Calabonga.UnitOfWork;
using Facts.Web.Mediatr.Base;
using Microsoft.Extensions.Logging;

namespace Facts.Web.Mediatr
{
    /// <summary>
    /// Custom manual notification
    /// </summary>
    public class ManualMessageNotification : NotificationBase
    {
        public ManualMessageNotification(string title, string content, string addressFrom, string addressTo)
            : base(title, content, addressFrom, addressTo, null)
        {
        }
    }

    public class ManualMessageNotificationHandler : NotificationHandlerBase<ManualMessageNotification>
    {
        public ManualMessageNotificationHandler(IUnitOfWork unitOfWork, ILogger<ManualMessageNotification> logger)
            : base(unitOfWork, logger)
        {
        }
    }
}