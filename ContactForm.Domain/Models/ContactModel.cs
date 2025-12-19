namespace ContactForm.Domain.Models
{
    public class ContactModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public DateTime CreatedAt { get; set; }

        public string? Phone { get; set; }
        public string? Website { get; set; }
        public string? Company { get; set; }
        public string? Address { get; set; }
    }
}
