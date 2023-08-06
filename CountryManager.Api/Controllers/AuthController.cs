using CountryManager.Domain.Interfaces;
using CountryManager.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CountryManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public AuthController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("sign-in")]
        public async Task<JwtToken> SignIn(CredentialsDto credentials)
        {
            return await _loginService.Login(credentials);
        }
    }
}
