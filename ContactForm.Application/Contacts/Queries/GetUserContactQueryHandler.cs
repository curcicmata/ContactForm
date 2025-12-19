using ContactForm.Application.Contacts.Queries.DTOs;
using ContactForm.Domain.Models;
using ContactForm.Domain.Repository;
using Microsoft.Extensions.Logging;

namespace ContactForm.Application.Contacts.Queries
{
    public class GetUserContactQueryHandler(IContactRepository repository, ILogger<GetUserContactQueryHandler> logger) : IGetUserContactQueryHandler
    {
        public async Task<ContactModel> HandleAsync(GetUserContactQuery query)
        {
            var userContact = await repository.GetUserContactAsync(query.Email);
            if (userContact is null)
            {
                logger.LogWarning("No contact found with email: {Email}", query.Email);
                throw new InvalidOperationException($"No contact found with email: {query.Email}");
            }

            logger.LogInformation("Retrieved contact for email: {Email}", query.Email);

            return userContact;
        }
    }
}
