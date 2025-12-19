namespace ContactForm.Application.Contacts.Commands.Create
{
    public record CreateContactCommand(
        string FirstName,
        string LastName,
        string Email
    );
}
