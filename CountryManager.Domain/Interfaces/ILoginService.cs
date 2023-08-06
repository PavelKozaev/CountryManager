using CountryManager.Shared.Dtos;

namespace CountryManager.Domain.Interfaces
{
    public interface ILoginService
    {
        Task<JwtToken> Login(CredentialsDto user);
    }
}
