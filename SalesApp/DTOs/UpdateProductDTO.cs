using System.ComponentModel.DataAnnotations;

namespace SalesApp.DTOs
{
    public class UpdateProductDTO
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Giá không được để trống")]
        [Range(1, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Số lượng không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int Stock { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "Ảnh sản phẩm không được để trống")]
        public string? ImageUrl { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn loại sản phẩm")]
        public int CategoryId { get; set; }
    }
}
