using ContactForm.Application.ExternalServices.Interface;
using Microsoft.Extensions.Logging;
using System.Net.Mail;

namespace ContactForm.Infrastructure.ExternalServices.Email
{
    public class SmtpEmailService(ILogger<SmtpEmailService> logger) : IEmailService
    {
        public async Task SendAsync(string subject, string body)
        {
            using var client = new SmtpClient("smtp.server.com");

            await client.SendMailAsync(
                new MailMessage("noreply@app.com", "admin@app.com", subject, body));

            logger.LogInformation("EMAIL SENT - Subject: {Subject}", subject);
        }
    }
}
