using CountryManager.Shared.Models;

namespace CountryManager.Shared.Dtos
{
    public class RegionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Code { get; set; } // Not in every country regions have codes
        public string? FlagUrl { get; set; }
        public int? Population { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
    }
}
