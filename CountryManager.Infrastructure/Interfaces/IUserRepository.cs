using CountryManager.Shared.Dtos;

namespace CountryManager.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDto> GetUserPassword(string userName);
    }
}
