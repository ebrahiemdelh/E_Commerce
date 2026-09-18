using E_Commerce.Application.Contracts.Dtos.Identity;

namespace E_Commerce.Application.Contracts
{
    public interface IUserStore
    {
        Task<Result<UserInfoDto>> GetByEmailAsync(string email, CancellationToken token = default);
        Task<Result<bool>> CheckPasswordAsync(string password, string email, CancellationToken token = default);
    }
}
