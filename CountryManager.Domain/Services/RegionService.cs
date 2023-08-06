using CountryManager.Domain.Interfaces;
using CountryManager.Infrastructure.Interfaces;
using CountryManager.Shared.Dtos;
using System.Text.RegularExpressions;

namespace CountryManager.Domain.Services
{
    public class RegionService : IRegionService
    {
        private const string REGION_CODE_PATTERN = @"^\d{2,3}$";

        private readonly IRegionRepository _regionRepository;

        public RegionService(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }

        public async Task<ItemsDto<RegionDto>> List(int page)
        {
            if (page < default(int))
                throw new ArgumentException("Page cannot be negative number");

            return await _regionRepository.List(page);
        }

        public async Task<RegionDto> Update(RegionDto region)
        {
            ValidateCountry(region);

            if (region.Id == default(int))
                return await _regionRepository.Create(region);
            else
                return await _regionRepository.Update(region);
        }

        private void ValidateCountry(RegionDto region)
        {
            ArgumentNullException.ThrowIfNull(region);

            Regex regex = new Regex(REGION_CODE_PATTERN);
            Match match = regex.Match(region.Code);

            if (!match.Success)
            {
                throw new ArgumentException("Invalid RegionCode");
            }

            if (string.IsNullOrWhiteSpace(region.Name))
                throw new ArgumentException("Invalid RegionName");
        }

        public async Task Delete(int id)
        {
            if (id <= default(int))
                throw new ArgumentException("Invalid id");

            await _regionRepository.Delete(id);
        }
    }
}
