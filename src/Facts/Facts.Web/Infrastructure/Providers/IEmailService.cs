using System.Threading;
using System.Threading.Tasks;

namespace Facts.Web.Infrastructure.Providers
{
    public interface IEmailService
    {
        Task<bool> SendAsync(EmailMessage emailMessage, CancellationToken cancellationToken);   
    }

    public class EmailService : IEmailService
    {
        public async Task<bool> SendAsync(EmailMessage emailMessage, CancellationToken cancellationToken)
        {
            await Task.Delay(2000);
            return true;
        }
    }
}