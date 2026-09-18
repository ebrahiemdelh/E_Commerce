using E_Commerce.Application.Contracts.Dtos.Identity;

namespace E_Commerce.Application.Contracts
{
    public interface IUserStore
    {
        Task<Result<UserInfoDto>> GetByEmailAsync(string email, CancellationToken token = default);
        Task<Result<bool>> CheckPasswordAsync(string password, string email, CancellationToken token = default);
        Task<Result<UserInfoDto>> CreateAsync(RegisterDto dto, CancellationToken token = default);
        Task<Result<List<string>>> GetRoles(string email);
        Task<Result<bool>> EmailExistsAsync(string email);

        Task<Result<AddressDto>> GetAddressAsync(string email);
        Task<Result<AddressDto>> UpsertAddressAsync(string email,AddressDto dto);
    }
}
