using ContactForm.Application.ExternalServices.Interface;
using ContactForm.Domain.Repository;
using Microsoft.Extensions.Logging;

namespace ContactForm.Application.Contacts.Commands.Delete
{
    public class DeleteContactCommandHandler(IContactRepository repository, IEmailService email, ILogger<DeleteContactCommandHandler> logger) : IDeleteContactCommandHandler
    {
        public async Task HandleAsync(DeleteContactCommand command)
        {
            logger.LogInformation("Processing deleting contact with email: {Email}", command.Email);

            var contactToDelete = await repository.GetUserContactAsync(command.Email);
            if (contactToDelete is null)
            {
                logger.LogWarning("No contact found with email: {Email}", command.Email);
                throw new InvalidOperationException($"No contact found with email: {command.Email}");
            }

            await repository.DeleteContactAsync(command.Email);

            await email.SendAsync(
                "Contact Deleted",
                $"The contact with email {command.Email} has been deleted."
            );

            logger.LogInformation("Contact deleted for email: {Email}", command.Email);
        }
    }
}
