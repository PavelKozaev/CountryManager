using CountryManager.Domain.Interfaces;
using CountryManager.Infrastructure.Interfaces;
using CountryManager.Shared.Dtos;

namespace CountryManager.Domain.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<ItemsDto<CityDto>> List(int page)
        {
            if (page < default(int))
                throw new ArgumentException("Page cannot be negative number");

            return await _cityRepository.List(page);
        }

        public async Task<CityDto> Update(CityDto city)
        {
            ValidateCountry(city);

            if (city.Id == default(int))
                return await _cityRepository.Create(city);
            else
                return await _cityRepository.Update(city);
        }

        private void ValidateCountry(CityDto city)
        {
            ArgumentNullException.ThrowIfNull(city);

            if (city.RegionId == default(int))
                throw new ArgumentException("Invalid RegionId");
        }

        public async Task Delete(int id)
        {
            if (id <= default(int))
                throw new ArgumentException("Invalid id");

            await _cityRepository.Delete(id);
        }
    }
}
