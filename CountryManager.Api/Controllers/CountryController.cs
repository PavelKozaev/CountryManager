using CountryManager.Domain.Interfaces;
using CountryManager.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CountryManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _service;

        public CountryController(ICountryService service)
        {
            _service = service;
        }

        [HttpGet("list"), AllowAnonymous]
        public async Task<ItemsDto<CountryDto>> List(int page) => await _service.List(page);

        [HttpPost("update")]
        public async Task<CountryDto> Update(CountryDto country) => await _service.Update(country);

        [HttpDelete("delete")]
        public async Task Delete(int id) => await _service.Delete(id);
    }
}
