using CountryManager.Shared.Dtos;

namespace CountryManager.Shared.Models
{
    public class Region
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string? Code { get; set; } // Not in every country regions have codes
        public string? FlagUrl { get; set; }
        public int? Population { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset UpdatedOn { get; set; }

        public Country Country { get; set; }

        public Region()
        {
        }

        public void Create(RegionDto region)
        {
            SetValues(region);
            CreatedOn = DateTimeOffset.Now;
            UpdatedOn = DateTimeOffset.Now;
        }

        public void Update(RegionDto region)
        {
            SetValues(region);
            UpdatedOn = DateTimeOffset.Now;
        }

        private void SetValues(RegionDto region)
        {
            Name = region.Name;
            CountryId = region.CountryId;
            FlagUrl = region.FlagUrl;
            Population = region.Population;
            Code = region.Code;
        }
    }
}
