using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Application.Contracts.Dtos.Identity
{
    public class LoginDto
    {
        [EmailAddress]
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
