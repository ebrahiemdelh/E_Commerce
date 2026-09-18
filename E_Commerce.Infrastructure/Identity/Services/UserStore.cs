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

        public async Task<Result<UserInfoDto>> CreateAsync(RegisterDto dto, CancellationToken token = default)
        {
            var user = new ApplicationUser
            {
                Email = dto.Email,
                DisplayName = dto.DisplayName,
                UserName = dto.UserName,
                PhoneNumber = dto.PhoneNumber
            };
            var result = await userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => Error.Failure(e.Code, e.Description)).ToList();
                return Result<UserInfoDto>.Fail(errors);
            }
            return new UserInfoDto
            {
                Id = user.Id,
                Email = user.Email,
                DisplayName = user.DisplayName,
                UserName = user.UserName,
            };
        }

        public async Task<Result<List<string>>> GetRoles(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user is null) return Error.NotFound("user not found", $"User With Email: {email} is Not Found");

            var roles = await userManager.GetRolesAsync(user);
            return roles.ToList();
        }

        public async Task<Result<bool>> EmailExistsAsync(string email)
        {
            return await userManager.FindByEmailAsync(email) is not null;
        }

        public async Task<Result<AddressDto>> GetAddressAsync(string email)
        {
            var user = await userManager.Users.Include(x => x.Address).FirstOrDefaultAsync(x => x.Email == email);
            if (user is null) return Error.NotFound("user not found", $"User With Email: {email} is Not Found");

            if (user.Address is null) return Error.NotFound("user address not found", $"User With Email: {email} doesn't have address");
            return new AddressDto
            {
                Street = user.Address.Street,
                City = user.Address.City,
                Country = user.Address.Country,
            };
        }

        public async Task<Result<AddressDto>> UpsertAddressAsync(string email, AddressDto dto)
        {
            var user = await userManager.Users.Include(x => x.Address).FirstOrDefaultAsync(x => x.Email == email);
            if (user is null) return Error.NotFound("user not found", $"User With Email: {email} is Not Found");

            if (user.Address is null)
            {
                user.Address = new Address
                {
                    Street = dto.Street,
                    City = dto.City,
                    Country = dto.Country,
                };
            }
            else
            {
                user.Address.Street = dto.Street;
                user.Address.City = dto.City;
                user.Address.Country = dto.Country;
            }
            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => Error.Failure(e.Code, e.Description)).ToList();
                return Result<AddressDto>.Fail(errors);
            }

            return new AddressDto
            {
                Street = user.Address.Street,
                City = user.Address.City,
                Country = user.Address.Country,
            };
        }
    }
}
