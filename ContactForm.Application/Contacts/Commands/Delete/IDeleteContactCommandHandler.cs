namespace ContactForm.Application.Contacts.Commands.Delete
{
    public interface IDeleteContactCommandHandler
    {
        Task HandleAsync(DeleteContactCommand command);
    }
}
