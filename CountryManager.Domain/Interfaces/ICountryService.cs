using CountryManager.Shared.Dtos;

namespace CountryManager.Domain.Interfaces
{
    public interface ICountryService
    {
        Task<ItemsDto<CountryDto>> List(int page);
        Task<CountryDto> Update(CountryDto country);
        Task Delete(int id);
    }
}
