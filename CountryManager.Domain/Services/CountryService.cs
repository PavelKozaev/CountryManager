using CountryManager.Domain.Interfaces;
using CountryManager.Infrastructure.Interfaces;
using CountryManager.Shared.Dtos;

namespace CountryManager.Domain.Services
{
    public class CountryService : ICountryService
    {
        private const int MAX_COUNTRY_CODE_LENGTH = 2;

        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<ItemsDto<CountryDto>> List(int page)
        {
            if (page < default(int))
                throw new ArgumentException("Page cannot be negative number");

            return await _countryRepository.List(page);
        }

        public async Task<CountryDto> Update(CountryDto country)
        {
            ValidateCountry(country);

            if (country.Id == default(int))
                return await _countryRepository.Create(country);
            else
                return await _countryRepository.Update(country);
        }

        private void ValidateCountry(CountryDto country)
        {
            ArgumentNullException.ThrowIfNull(country);

            if (country.CountryCode?.Length > MAX_COUNTRY_CODE_LENGTH)
                throw new ArgumentException("Invalid CountryCode");

            if (string.IsNullOrWhiteSpace(country.Name))
                throw new ArgumentException("Invalid CountryName");
        }

        public async Task Delete(int id)
        {
            if(id <= default(int))
                throw new ArgumentException("Invalid id");

            await _countryRepository.Delete(id);
        }
    }
}
