using ContactForm.Application.ExternalServices.Interface;
using Microsoft.Extensions.Logging;

namespace ContactForm.Infrastructure.ExternalServices.Email
{
    public class MockSmtpEmailService(ILogger<SmtpEmailService> logger) : IEmailService
    {
        public async Task SendAsync(string subject, string body)
        {
            logger.LogInformation("MOCK EMAIL - Subject: {Subject}", subject);
            logger.LogInformation("MOCK EMAIL - Body: {Body}", body);

            await Task.CompletedTask;
        }
    }
}
