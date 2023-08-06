using CountryManager.Shared.Dtos;

namespace CountryManager.Domain.Interfaces
{
    public interface IRegionService
    {
        Task<ItemsDto<RegionDto>> List(int page);
        Task<RegionDto> Update(RegionDto country);
        Task Delete(int id);
    }
}
