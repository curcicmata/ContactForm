namespace ContactForm.Application.ExternalServices.Interface
{
    public interface IEmailService
    {
        Task SendAsync(string subject, string body);
    }
}
