using ContactForm.Application.Contacts.Queries.DTOs;
using ContactForm.Domain.Models;

namespace ContactForm.Application.Contacts.Queries
{
    public interface IGetUserContactQueryHandler
    {
        Task<ContactModel> HandleAsync(GetUserContactQuery query);
    }
}
