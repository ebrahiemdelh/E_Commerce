namespace E_Commerce.Application.Contracts.Dtos.Identity
{
    public class UserInfoDto
    {
        public string Id { get; set; } = default!;
        public string DisplayName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? UserName { get; set; } = default!;
    }
}
