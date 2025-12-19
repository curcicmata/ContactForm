namespace ContactForm.Infrastructure.ExternalServices.JsonPlaceHolder.DTOs
{
    internal class JsonUser
    {
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string Website { get; set; } = default!;
        public CompanyDto Company { get; set; } = default!;
        public AddressDto Address { get; set; } = default!;
    }
}
