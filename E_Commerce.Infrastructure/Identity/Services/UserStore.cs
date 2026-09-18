using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.Contracts.Dtos.Identity;
using E_Commerce.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;


namespace E_Commerce.Infrastructure.Identity.Services
{
    public class UserStore(UserManager<ApplicationUser> userManager) : IUserStore
    {
        public async Task<Result<UserInfoDto>> GetByEmailAsync(string email, CancellationToken token = default)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user is null) return Error.NotFound("user not found", $"User With Email: {email} is Not Found");

            return new UserInfoDto
            {
                Id = user.Id,
                Email = email,
                DisplayName = user.DisplayName,
                UserName = user.UserName,
            };

        }

        public async Task<Result<bool>> CheckPasswordAsync(string password, string email, CancellationToken token = default)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user is null) return Error.NotFound("user not found", $"User With Email: {email} is Not Found");

            return await userManager.CheckPasswordAsync(user, password);
        }
    }
}
