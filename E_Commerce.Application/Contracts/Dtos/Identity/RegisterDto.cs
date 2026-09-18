using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Application.Contracts.Dtos.Identity
{
    public class RegisterDto
    {
        [EmailAddress]
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string DisplayName { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string? PhoneNumber { get; set; } = default!;
    }
}
