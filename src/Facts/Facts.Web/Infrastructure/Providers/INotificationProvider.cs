using AutoMapper;
using Calabonga.UnitOfWork;
using Facts.Web.Data.Dto;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Facts.Web.Infrastructure.Providers
{
    public interface INotificationProvider
    {
        Task ProcessAsync(CancellationToken token);
    }

    public class NotificationProvider : INotificationProvider
    {
        private IEmailService emailService;
        private IUnitOfWork unitOfWork;
        private IMapper mapper;

        public NotificationProvider(IEmailService emailService,
                                    IUnitOfWork unitOfWork, 
                                    IMapper mapper)
        {
            this.emailService = emailService;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task ProcessAsync(CancellationToken token)
        {
            var repository = unitOfWork.GetRepository<Notification>();
            var items = repository.GetAll(
                            predicate: x => !x.IsCompleted,
                            orderBy: o => o.OrderBy(x => x.CreatedAt)).ToList();
            if(!items.Any())
            {
                return;
            }
            var emails = mapper.Map<IEnumerable<EmailMessage>>(items);
            foreach (var email in emails)
            {
                var isSent = await emailService.SendAsync(email, token);
                if(isSent)
                {
                    NotificationCopmleted(Guid.Parse(email.Id));
                }
            }
        }

        private void NotificationCopmleted(Guid guid)
        {
            var repository = unitOfWork.GetRepository<Notification>();
            var notification = repository.GetFirstOrDefault(predicate: x => x.Id == guid,
                                                            disableTracking: false);
            if (notification == null)
                return;
            notification.IsCompleted = true;
            repository.Update(notification);
            unitOfWork.SaveChanges();
        }
    }
}