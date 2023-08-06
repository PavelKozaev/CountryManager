using CountryManager.Shared.Dtos;

namespace CountryManager.Infrastructure.Interfaces
{
    public interface IRegionRepository
    {
        Task<ItemsDto<RegionDto>> List(int page);
        Task<RegionDto> Create(RegionDto country);
        Task<RegionDto> Update(RegionDto country);
        Task Delete(int id);
    }
}
