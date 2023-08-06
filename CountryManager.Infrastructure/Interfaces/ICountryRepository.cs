using CountryManager.Shared.Dtos;

namespace CountryManager.Infrastructure.Interfaces
{
    public interface ICountryRepository
    {
        Task<ItemsDto<CountryDto>> List(int page);
        Task<CountryDto> Create(CountryDto country);
        Task<CountryDto> Update(CountryDto country);
        Task Delete(int id);
    }
}
