using CountryManager.Infrastructure.Interfaces;
using CountryManager.Shared.Dtos;
using CountryManager.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CountryManager.Infrastructure.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;

        public int PageSize => int.Parse(_config["PageSize"]);

        public RegionRepository(DataContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<ItemsDto<RegionDto>> List(int page)
        {
            var regions = await _context.Regions
                .Skip(page * PageSize)
                .Take(PageSize)
                .Select(r => new RegionDto
                {
                    Id = r.Id,
                    Code = r.Code,
                    CountryId = r.CountryId,
                    CountryName = r.Country.Name,
                    FlagUrl = r.FlagUrl,
                    Name = r.Name,
                    Population = r.Population
                })
                .ToListAsync();

            var numberOfPages = (int)Math.Ceiling((decimal)(await _context.Regions.CountAsync()) / (decimal)PageSize);

            return new ItemsDto<RegionDto>
            {
                Items = regions,
                NumberOfPages = numberOfPages
            };
        }

        public async Task<RegionDto> Create(RegionDto region)
        {
            var entity = new Region();
            entity.Create(region);

            await _context.Regions.AddAsync(entity);
            await _context.SaveChangesAsync();

            region.Id = entity.Id;

            return region;
        }

        public async Task<RegionDto> Update(RegionDto region)
        {
            var existingRegion = await _context.Regions
                .FirstOrDefaultAsync(c => c.Id == region.Id)
                ?? throw new ApplicationException("Region does not exist");

            existingRegion.Update(region);

            await _context.SaveChangesAsync();

            return region;
        }

        public async Task Delete(int id)
        {
            var existingRegion = await _context.Regions.FirstOrDefaultAsync(c => c.Id == id);

            if (existingRegion is null)
                return;

            _context.Regions.Remove(existingRegion);
            await _context.SaveChangesAsync();
        }
    }
}
