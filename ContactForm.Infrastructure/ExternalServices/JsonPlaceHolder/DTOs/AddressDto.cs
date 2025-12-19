namespace ContactForm.Infrastructure.ExternalServices.JsonPlaceHolder.DTOs
{
    public class AddressDto
    {
        public string Street { get; set; } = default!;
        public string Suite { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Zipcode { get; set; } = default!;
        public GeoDto Geo { get; set; } = default!;
    }
}
