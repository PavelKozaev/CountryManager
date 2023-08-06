using CountryManager.Infrastructure.Interfaces;
using CountryManager.Shared.Dtos;
using CountryManager.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CountryManager.Infrastructure.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;

        public int PageSize => int.Parse(_config["PageSize"]);

        public CityRepository(DataContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<ItemsDto<CityDto>> List(int page)
        {
            var cities = await _context.Cities
                .Skip(page * PageSize)
                .Take(PageSize)
                .Select(r => new CityDto
                {
                    Id = r.Id,
                    CountryId = r.Region.CountryId,
                    CountryName = r.Region.Country.Name,
                    Name = r.Name,
                    Population = r.Population,
                    RegionId = r.RegionId,
                    RegionName = r.Region.Name
                })
                .ToListAsync();

            var numberOfPages = (int)Math.Ceiling((decimal)(await _context.Cities.CountAsync()) / (decimal)PageSize);

            return new ItemsDto<CityDto>
            {
                Items = cities,
                NumberOfPages = numberOfPages
            };
        }

        public async Task<CityDto> Create(CityDto city)
        {
            var entity = new City();
            entity.Create(city);

            await _context.Cities.AddAsync(entity);
            await _context.SaveChangesAsync();

            city.Id = entity.Id;

            return city;
        }

        public async Task<CityDto> Update(CityDto city)
        {
            var existingCity = await _context.Cities
                .FirstOrDefaultAsync(c => c.Id == city.Id)
                ?? throw new ApplicationException("City does not exist");

            existingCity.Update(city);

            await _context.SaveChangesAsync();

            return city;
        }

        public async Task Delete(int id)
        {
            var existingCity = await _context.Cities.FirstOrDefaultAsync(c => c.Id == id);

            if (existingCity is null)
                return;

            _context.Cities.Remove(existingCity);
            await _context.SaveChangesAsync();
        }
    }
}
