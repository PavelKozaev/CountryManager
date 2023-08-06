using CountryManager.Shared.Dtos;

namespace CountryManager.Infrastructure.Interfaces
{
    public interface ICityRepository
    {
        Task<ItemsDto<CityDto>> List(int page);
        Task<CityDto> Create(CityDto city);
        Task<CityDto> Update(CityDto city);
        Task Delete(int id);
    }
}
