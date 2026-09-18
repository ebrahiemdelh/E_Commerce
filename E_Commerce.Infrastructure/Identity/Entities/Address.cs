namespace E_Commerce.Infrastructure.Identity.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Country { get; set; } = default!;

        public ApplicationUser User { get; set; } = default!;
        public string UserId { get; set; } = default!;
    }
}