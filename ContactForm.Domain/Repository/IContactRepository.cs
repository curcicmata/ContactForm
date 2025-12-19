using ContactForm.Domain.Models;

namespace ContactForm.Domain.Repository
{
    public interface IContactRepository
    {
        Task DeleteContactAsync(string email);
        Task<bool> ExistsWithinLastMinuteAsync(string email);
        Task<ContactModel> GetUserContactAsync(string email);
        Task SaveAsync(ContactModel contact);
    }
}
