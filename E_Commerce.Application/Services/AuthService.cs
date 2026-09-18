using E_Commerce.Application.Contracts.Dtos.Identity;

namespace E_Commerce.Application.Services
{
    public class AuthService(IUserStore userStore) : IAuthService
    {
        public async Task<Result<UserDto>> LoginAsync(LoginDto dto, CancellationToken token = default)
        {
            var userInfo = await userStore.GetByEmailAsync(dto.Email);
            if (userInfo.IsFailure) return Error.Failure();

            var passwordResult = await userStore.CheckPasswordAsync(dto.Password, dto.Email);
            if (userInfo.IsFailure) return Error.Failure();

            //Todo: Generate JWT Token then add it to response

            return new UserDto
            {
                Email = userInfo.Value.Email,
                DisplayName = userInfo.Value.DisplayName,
                Token = "This Will be discussed"
            };

        }

        public Task<Result<UserDto>> RegisterAsync(RegisterDto dto, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> EmailExistsAsync(string email, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}
