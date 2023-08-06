using CountryManager.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CountryManager.Domain.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;

        private SymmetricSecurityKey AuthKey => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
        private int AccessLifetime => int.Parse(_config["JWT:AccessLifetime"]);

        public TokenService(IConfiguration config)
        {
            _config = config;
        }
        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
               claims: claims,
               expires: DateTime.Now.AddHours(AccessLifetime),
               signingCredentials: new SigningCredentials(AuthKey, SecurityAlgorithms.HmacSha256)
           );

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
    }
}
