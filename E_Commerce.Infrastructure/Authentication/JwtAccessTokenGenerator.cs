using E_Commerce.Application.Contracts;
using E_Commerce.Application.Contracts.Dtos.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_Commerce.Infrastructure.Authentication
{
    public class JwtAccessTokenGenerator(IOptions<JwtSettings> options) : IAccessTokenService
    {
        private readonly JwtSecurityTokenHandler handler = new();
        public string Generate(UserInfoDto dto, IEnumerable<string> roles)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            if (dto.Email is null) throw new ArgumentException("Email is Required", nameof(dto.Email));

            if (dto.UserName is null) throw new ArgumentException("UserName is Required", nameof(dto.UserName));

            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, dto.Id),
                new Claim(ClaimTypes.Name, dto.DisplayName),
                new Claim(ClaimTypes.Email, dto.Email),
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));



            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.Key));
            var credits = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: options.Value.Issuer,
                audience: options.Value.Audience,
                expires: DateTime.Now.AddMinutes(options.Value.ExpireMiutes),
                claims: claims,
                signingCredentials: credits);
            //return token.RawData;
            return handler.WriteToken(token);
        }
    }
}
