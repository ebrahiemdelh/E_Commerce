using E_Commerce.API.Controllers.Base;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.Contracts.Dtos.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce.API.Controllers
{
    public class AuthController(IAuthService authService) : APIBaseController
    {
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> LoginAsync(LoginDto dto, CancellationToken token = default)
        {
            return HandleResult(await authService.LoginAsync(dto, token));
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> RegisterAsync(RegisterDto dto, CancellationToken token = default)
        {
            return HandleResult(await authService.RegisterAsync(dto, token));
        }

        [HttpGet("emailExists")]
        public async Task<ActionResult<bool>> EmailExistsAsync([FromQuery] string email)
        {
            return HandleResult(await authService.EmailExistsAsync(email));
        }
        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetAddressAsync()
        {
            string email = User?.FindFirstValue(ClaimTypes.Email)!;
            return HandleResult(await authService.GetAddressAsync(email));
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpsertAddressAsync(AddressDto dto)
        {
            string email = User?.FindFirstValue(ClaimTypes.Email)!;
            return HandleResult(await authService.UpsertAddressAsync(email, dto));
        }

        [Authorize]
        [HttpGet("current-user")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            string email = User?.FindFirstValue(ClaimTypes.Email)!;
            var result = await authService.GetCurrentUserAsync(email);
        }
    }
}
