using CountryManager.Shared.Dtos;

namespace CountryManager.Shared.Models
{
    public class City
    {
        public int Id { get; set; }
        public int RegionId { get; set; }
        public string Name { get; set; }
        public int? Population { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset UpdatedOn { get; set; }

        public Region Region { get; set; }

        public City()
        {
        }

        public void Create(CityDto city)
        {
            SetValues(city);
            CreatedOn = DateTimeOffset.Now;
            UpdatedOn = DateTimeOffset.Now;
        }

        public void Update(CityDto city)
        {
            SetValues(city);
            UpdatedOn = DateTimeOffset.Now;
        }

        private void SetValues(CityDto city)
        {
            Name = city.Name;
            RegionId = city.RegionId;
            Population = city.Population;
        }
    }
}
