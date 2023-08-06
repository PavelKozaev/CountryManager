using CountryManager.Shared.Dtos;

namespace CountryManager.Shared.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public string? FlagUrl { get; set; }
        public int? Population { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset UpdatedOn { get; set; }

        public Country()
        {}

        public void Create(CountryDto country)
        {
            SetValues(country);
            CreatedOn = DateTimeOffset.Now;
            UpdatedOn = DateTimeOffset.Now;
        }

        public void Update(CountryDto country)
        {
            SetValues(country);
            UpdatedOn = DateTimeOffset.Now;
        }

        private void SetValues(CountryDto country)
        {
            Name = country.Name;
            CountryCode = country.CountryCode;
            FlagUrl = country.FlagUrl;
            Population = country.Population;
        }
    }
}
