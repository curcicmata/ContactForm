using ContactForm.Application.ExternalServices.DTOs;

namespace ContactForm.Application.ExternalServices.Interface
{
    public interface IUserEnrichmentService
    {
        Task<UserEnrichmentResult?> GetByEmailAsync(string email);
    }
}
