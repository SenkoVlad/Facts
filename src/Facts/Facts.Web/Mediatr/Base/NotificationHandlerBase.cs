using Calabonga.UnitOfWork;
using Facts.Web.Data.Dto;
using Facts.Web.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Facts.Web.Mediatr.Base
{
    public abstract class NotificationHandlerBase<T> : INotificationHandler<T> where T : NotificationBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger logger;
        protected NotificationHandlerBase(IUnitOfWork unitOfWork, ILogger<T> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }
        public async Task Handle(T notification, CancellationToken cancellationToken)
        {
            var repository = unitOfWork.GetRepository<Notification>();
            
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(notification.Content);
            
            if(notification.Exception is not null)
            {
                stringBuilder.Append(notification.Exception.Message);

                if(notification.Exception.InnerException is not null)
                    stringBuilder.Append(notification.Exception.InnerException.Message);
                
                stringBuilder.AppendLine(notification.Exception.GetBaseException().Message);
                stringBuilder.AppendLine(notification.Exception.StackTrace);
            }

            var entity = new Notification(notification.Subject, stringBuilder.ToString(), notification.AddressFrom, notification.AddressTo);
            await repository.InsertAsync(entity);
            await unitOfWork.SaveChangesAsync();

            if(!unitOfWork.LastSaveChangesResult.IsOk)
            {
                logger.LogDababaseSavingError(nameof(Notification), unitOfWork.LastSaveChangesResult.Exception);
                return;
            }
            logger.NotificationAdded(notification.Subject);
        }
    }
}
