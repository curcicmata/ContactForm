using ContactForm.Application.ExternalServices.Interface;
using ContactForm.Domain.Models;
using ContactForm.Domain.Repository;
using Microsoft.Extensions.Logging;

namespace ContactForm.Application.Contacts.Commands.Create
{
    public class CreateContactCommandHandler(
        IContactRepository repository,
        IUserEnrichmentService enrichment,
        IEmailService email,
        ILogger<CreateContactCommandHandler> logger) : ICreateContactCommandHandler
    {
        private readonly IUserEnrichmentService _enrichment = enrichment;

        public async Task HandleAsync(CreateContactCommand command)
        {
            logger.LogInformation("Processing contact submission for {Email}", command.Email);

            //if (await repository.ExistsWithinLastMinuteAsync(command.Email))
            //{
            //    logger.LogWarning("Spam prevention triggered for {Email}", command.Email);
            //    throw new InvalidOperationException("You can only submit once per minute.");
            //}

            var submission = new ContactModel
            {
                Id = Guid.NewGuid(),
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                CreatedAt = DateTime.UtcNow
            };

            var enrichment = await _enrichment.GetByEmailAsync(command.Email);
            if (enrichment is not null)
            {
                submission.Phone = enrichment.Phone;
                submission.Website = enrichment.Website;
                submission.Company = enrichment.Company;
                submission.Address = enrichment.Address;
            }

            await repository.SaveAsync(submission);

            await email.SendAsync(
                "New Contact Submission",
                $"Name: {submission.FirstName} {submission.LastName}\nEmail: {submission.Email}\nPhone: {submission.Phone}"
            );

            logger.LogInformation("Contact submission saved successfully for {Email}", command.Email);
        }
    }

}
