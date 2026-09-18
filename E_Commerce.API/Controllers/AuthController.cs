using E_Commerce.API.Controllers.Base;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.Contracts.Dtos.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    public class AuthController(IAuthService authService) : APIBaseController
    {
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> LoginAsync(LoginDto dto, CancellationToken token = default)
        {
            return HandleResult(await authService.LoginAsync(dto, token));
        }
    }
}
