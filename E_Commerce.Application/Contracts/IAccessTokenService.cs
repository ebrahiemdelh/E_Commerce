using E_Commerce.Application.Contracts.Dtos.Identity;

namespace E_Commerce.Application.Contracts
{
    public interface IAccessTokenService
    {
        string Generate(UserInfoDto dto, IEnumerable<string> roles);
    }
}
