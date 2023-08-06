using CountryManager.Domain.Interfaces;
using CountryManager.Infrastructure.Interfaces;
using CountryManager.Shared.Dtos;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace CountryManager.Domain.Services
{
    public class LoginService : ILoginService
    {
        private const string USER_ID_KEY = "userId";        

        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _config;
                

        public LoginService(
            IUserRepository userRepository, ITokenService tokenService,
            IConfiguration config)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _config = config;
        }

        public async Task<JwtToken> Login(CredentialsDto user)
        {
            if (user is null || string.IsNullOrEmpty((string)user.UserName) || string.IsNullOrEmpty((string)user.Password))
            {
                throw new ApplicationException("Empty login data");
            }

            var userData = await _userRepository.GetUserPassword((string)user.UserName);

            // In real app I would use Hash here, but for purpose of test task I decided to ommit it
            if (userData is null || userData.Password != user.Password) 
            {
                return new JwtToken();
            }

            return new JwtToken
            {
                Jwt = _tokenService.GenerateAccessToken(new List<Claim>
                {
                    new Claim(USER_ID_KEY, userData.Id.ToString()),
                    new Claim(ClaimTypes.Role, userData.Role),
                }),
                Refresh = null // Omitted fo purpose of this task
            };
        }
    }
}
