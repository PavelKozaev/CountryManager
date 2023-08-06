namespace CountryManager.Shared.Dtos
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Population { get; set; }
        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
    }
}
