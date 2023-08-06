using CountryManager.Domain.Interfaces;
using CountryManager.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CountryManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class CityController : ControllerBase
    {
        private readonly ICityService _service;

        public CityController(ICityService service)
        {
            _service = service;
        }

        [HttpGet("list"), AllowAnonymous]
        public async Task<ItemsDto<CityDto>> List(int page) => await _service.List(page);

        [HttpPost("update")]
        public async Task<CityDto> Update(CityDto country) => await _service.Update(country);

        [HttpDelete("delete")]
        public async Task Delete(int id) => await _service.Delete(id);
    }
}
