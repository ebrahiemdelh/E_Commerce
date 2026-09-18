namespace E_Commerce.Application.Contracts.Dtos.Identity
{
    public class AddressDto
    {
        public string Street { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Country { get; set; } = default!;
    }
}
