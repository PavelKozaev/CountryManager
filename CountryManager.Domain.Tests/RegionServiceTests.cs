using CountryManager.Domain.Interfaces;
using CountryManager.Domain.Services;
using CountryManager.Infrastructure.Interfaces;
using CountryManager.Shared.Dtos;
using Moq;

namespace CountryManager.Domain.Tests
{
    public class RegionServiceTests
    {
        private RegionDto UpdatedRegion = new RegionDto
        {
            Id = 1,
            Code = "15",
            CountryId = 1,
            Name = "Республика Северная Осетия — Алания",
            FlagUrl = "https://mysuperurl.ru/images/sev_oset.png"
        };

        private readonly Mock<IRegionRepository> _repo;

        public RegionServiceTests()
        {
            _repo = new Mock<IRegionRepository>();
            _repo.Setup(_ => _.Update(It.IsAny<RegionDto>())).ReturnsAsync(UpdatedRegion);
            _repo.Setup(_ => _.Create(It.IsAny<RegionDto>())).ReturnsAsync(UpdatedRegion);
        }

        private IRegionService GetService() => new RegionService(_repo.Object);

        [Fact]
        public async Task Update_NullDto_ArgumentNullExceptionExpected()
        {
            var service = GetService();

            await Assert.ThrowsAsync<ArgumentNullException>(() => service.Update(null));
        }

        [Fact]
        public async Task Update_CodeIsEmpty_ArgumentExceptionExpected()
        {
            var service = GetService();

            UpdatedRegion.Code = "    ";

            await Assert.ThrowsAsync<ArgumentException>(() => service.Update(UpdatedRegion));
        }

        [Fact]
        public async Task Update_CodeContainsLetters_ArgumentExceptionExpected()
        {
            var service = GetService();

            UpdatedRegion.Code = "15A";

            await Assert.ThrowsAsync<ArgumentException>(() => service.Update(UpdatedRegion));
        }

        [Fact]
        public async Task Update_CodeTooLong_ArgumentExceptionExpected()
        {
            var service = GetService();

            UpdatedRegion.Code = "1589";

            await Assert.ThrowsAsync<ArgumentException>(() => service.Update(UpdatedRegion));
        }

        [Fact]
        public async Task Update_CodeOK_Success()
        {
            var service = GetService();

            var result = await service.Update(UpdatedRegion);

            Assert.Equal(UpdatedRegion.Id, result.Id);
        }

        [Fact]
        public async Task Update_CodeOK_CreateNew_Success()
        {
            var service = GetService();

            var updatedDto = new RegionDto
            {
                Id = 0,
                Code = UpdatedRegion.Code,
                CountryId = UpdatedRegion.CountryId,
                Name = UpdatedRegion.Name,
                FlagUrl = UpdatedRegion.FlagUrl
            };

            var result = await service.Update(updatedDto);

            Assert.Equal(1, result.Id);
            Assert.Equal(UpdatedRegion.Code, result.Code);
        }
    }
}