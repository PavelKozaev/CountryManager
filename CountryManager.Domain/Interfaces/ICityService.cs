using CountryManager.Shared.Dtos;

namespace CountryManager.Domain.Interfaces
{
    public interface ICityService
    {
        Task<ItemsDto<CityDto>> List(int page);
        Task<CityDto> Update(CityDto city);
        Task Delete(int id);
    }
}
