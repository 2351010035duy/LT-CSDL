using System.ComponentModel.DataAnnotations;

namespace SalesApp.DTOs
{
    public class CreateCategoryDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
