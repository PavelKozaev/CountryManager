using CountryManager.Infrastructure.Interfaces;
using CountryManager.Shared.Dtos;
using CountryManager.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CountryManager.Infrastructure.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;

        public int PageSize => int.Parse(_config["PageSize"]);

        public CountryRepository(DataContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<ItemsDto<CountryDto>> List(int page)
        {
            var countries = await _context.Countries
                .Skip(page * PageSize)
                .Take(PageSize)
                .Select(c => new CountryDto(c))
                .ToListAsync();

            var numberOfPages = (int)Math.Ceiling((decimal)(await _context.Countries.CountAsync()) / (decimal)PageSize);

            return new ItemsDto<CountryDto>
            {
                Items = countries,
                NumberOfPages = numberOfPages
            };
        }

        public async Task<CountryDto> Create(CountryDto country)
        {
            var entity = new Country();
            entity.Create(country);

            await _context.Countries.AddAsync(entity);
            await _context.SaveChangesAsync();

            country.Id = entity.Id;

            return country;
        }

        public async Task<CountryDto> Update(CountryDto country)
        {
            var existingCountry = await _context.Countries
                .FirstOrDefaultAsync(c => c.Id == country.Id)
                ?? throw new ApplicationException("Country does not exist");

            existingCountry.Update(country);

            await _context.SaveChangesAsync();

            return country;
        }

        public async Task Delete(int id)
        {
            var existingCountry = await _context.Countries.FirstOrDefaultAsync(c => c.Id == id);

            if (existingCountry is null)
                return;

            _context.Countries.Remove(existingCountry);
            await _context.SaveChangesAsync();
        }
    }
}
