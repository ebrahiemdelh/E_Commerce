using E_Commerce.Application.Contracts.Dtos.Identity;

namespace E_Commerce.Application.Services
{
    public class AuthService(IUserStore userStore, IAccessTokenService accessToken) : IAuthService
    {
        public async Task<Result<UserDto>> LoginAsync(LoginDto dto, CancellationToken token = default)
        {
            var userInfo = await userStore.GetByEmailAsync(dto.Email);
            if (userInfo.IsFailure) return Error.Failure();

            var passwordResult = await userStore.CheckPasswordAsync(dto.Password, dto.Email);
            if (userInfo.IsFailure) return Error.Failure();

            var rolesResult = await userStore.GetRoles(dto.Email);
            if (rolesResult.IsFailure)
                return Result<UserDto>.Fail(rolesResult.Errors.ToList());

            return new UserDto
            {
                Email = userInfo.Value.Email,
                DisplayName = userInfo.Value.DisplayName,
                Token = accessToken.Generate(userInfo.Value, rolesResult.Value)
            };

        }

        public async Task<Result<UserDto>> RegisterAsync(RegisterDto dto, CancellationToken token = default)
        {
            var result = await userStore.CreateAsync(dto, token);
            if (result.IsFailure) return Result<UserDto>.Fail(result.Errors.ToList());

            var rolesResult = await userStore.GetRoles(dto.Email);
            if (rolesResult.IsFailure)
                return Result<UserDto>.Fail(rolesResult.Errors.ToList());

            return new UserDto
            {
                DisplayName = result.Value.DisplayName,
                Email = result.Value.Email,
                Token = accessToken.Generate(result.Value, rolesResult.Value)
            };
        }

        public Task<Result<bool>> EmailExistsAsync(string email, CancellationToken token = default)
        {
            return userStore.EmailExistsAsync(email);
        }

        public async Task<Result<AddressDto>> GetAddressAsync(string email)
        {
            var result = await userStore.GetAddressAsync(email);
            if (result.IsFailure) return Result<AddressDto>.Fail(result.Errors.ToList());
            return new AddressDto
            {
                Street = result.Value.Street,
                City = result.Value.City,
                Country = result.Value.Country,
            };
        }

        public async Task<Result<AddressDto>> UpsertAddressAsync(string email, AddressDto dto)
        {
            var result = await userStore.UpsertAddressAsync(email, dto);
            if (result.IsFailure) return Result<AddressDto>.Fail(result.Errors.ToList());
            return new AddressDto
            {
                Street = result.Value.Street,
                City = result.Value.City,
                Country = result.Value.Country,
            };
        }

        public async Task<Result<UserDto>> GetCurrentUserAsync(string email)
        {
            var userInfo = await userStore.GetByEmailAsync(email);
            if (userInfo.IsFailure) return Error.Failure();

            var rolesResult = await userStore.GetRoles(email);
            if (rolesResult.IsFailure)
                return Result<UserDto>.Fail(rolesResult.Errors.ToList());

            return new UserDto
            {
                Email = userInfo.Value.Email,
                DisplayName = userInfo.Value.DisplayName,
                Token = accessToken.Generate(userInfo.Value, rolesResult.Value)
            };
        }
    }
}
