namespace ContactForm.Application.Contacts.Commands.Create
{
    public interface ICreateContactCommandHandler
    {
        Task HandleAsync(CreateContactCommand command);
    }
}
