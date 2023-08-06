using CountryManager.Domain.Interfaces;
using CountryManager.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CountryManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class RegionController : ControllerBase
    {
        private readonly IRegionService _service;

        public RegionController(IRegionService service)
        {
            _service = service;
        }

        [HttpGet("list"), AllowAnonymous]
        public async Task<ItemsDto<RegionDto>> List(int page) => await _service.List(page);

        [HttpPost("update")]
        public async Task<RegionDto> Update(RegionDto country) => await _service.Update(country);

        [HttpDelete("delete")]
        public async Task Delete(int id) => await _service.Delete(id);
    }
}
