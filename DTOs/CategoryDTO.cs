namespace SalesApp.DTOs
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public List<string> Products { get; set; }
    }
}
