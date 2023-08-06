using System.Security.Claims;

namespace CountryManager.Domain.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
    }
}
