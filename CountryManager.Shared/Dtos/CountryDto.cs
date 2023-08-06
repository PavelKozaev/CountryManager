using CountryManager.Shared.Models;

namespace CountryManager.Shared.Dtos
{
    public class CountryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public string? FlagUrl { get; set; }
        public int? Population { get; set; }

        public CountryDto()
        {
        }

        public CountryDto(Country country)
        {
            Id = country.Id;
            Name = country.Name;
            CountryCode = country.CountryCode;
            FlagUrl = country.FlagUrl;
            Population = country.Population;
        }
    }
}
