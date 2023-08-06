using CountryManager.Infrastructure.Interfaces;
using CountryManager.Shared.Dtos;
using Microsoft.EntityFrameworkCore;

namespace CountryManager.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<UserDto> GetUserPassword(string userName)
        {
            return await _context.Users
                .Where(u => u.UserName == userName)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Password = u.Password,
                    Role = u.Role.Code
                })
                .FirstOrDefaultAsync();
        }
    }
}
