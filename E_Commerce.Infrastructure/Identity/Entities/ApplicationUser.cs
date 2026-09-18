using Microsoft.AspNetCore.Identity;
using System.Net.Sockets;

namespace E_Commerce.Infrastructure.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; } = default!;
        public Address? Address { get; set; }
    }
}