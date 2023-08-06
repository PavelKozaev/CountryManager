namespace CountryManager.Shared.Dtos
{
    public class ItemsDto<T>
    {
        public List<T> Items { get; set; }
        public int NumberOfPages { get; set; }
    }
}
