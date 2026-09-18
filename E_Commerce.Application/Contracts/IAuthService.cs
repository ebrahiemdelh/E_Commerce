using E_Commerce.Application.Contracts.Dtos.Identity;

namespace E_Commerce.Application.Contracts
{
    public interface IAuthService
    {
        Task<Result<UserDto>> LoginAsync(LoginDto dto, CancellationToken token = default);
        Task<Result<UserDto>> RegisterAsync(RegisterDto dto, CancellationToken token = default);
        Task<Result<bool>> EmailExistsAsync(string email, CancellationToken token = default);
        Task<Result<AddressDto>> GetAddressAsync(string email);
        Task<Result<AddressDto>> UpsertAddressAsync(string email, AddressDto dto);
        Task<Result<UserDto>> GetCurrentUserAsync(string email);
    }
}
