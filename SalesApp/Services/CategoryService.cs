using SalesApp.DTOs;
using SalesApp.Models;
using SalesApp.Repositories;

namespace SalesApp.Services
{
    public class CategoryService
    {
        private readonly CategoryRepository _repo;

        public CategoryService(CategoryRepository repo)
        {
            _repo = repo;
        }

        public List<CategoryDTO> GetAll()
        {
            var categories = _repo.GetAll();

            return categories.Select(c => new CategoryDTO
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Products = c.Products.Select(p => p.Name).ToList()
            }).ToList();
        }

        public CategoryDTO Get(int id)
        {
            var c = _repo.GetById(id);

            if (c == null) return null;

            return new CategoryDTO
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Products = c.Products.Select(p => p.Name).ToList()
            };
        }

        public void Create(CreateCategoryDTO dto)
        {
            var exists = _repo.GetAll().Any(c => c.Name == dto.Name);

            if (exists)
                throw new Exception("Loại đã tồn tại");

            if (string.IsNullOrEmpty(dto.Name))
                throw new Exception("Tên loại không được để trống");

            var category = new Category
            {
                Name = dto.Name,
                Products = new List<Product>()
            };

            _repo.Add(category);
        }

        public void Update(UpdateCategoryDTO dto)
        {
            var category = _repo.GetById(dto.CategoryId);

            if (category == null)
                throw new Exception("Loại sản phẩm không tồn tại!");

            var exists = _repo.GetAll().Any(c => c.Name == dto.Name);

            if (exists)
                throw new Exception("Loại đã tồn tại");

            category.Name = dto.Name;

            _repo.Update(category);
        }

        public void Delete(int id)
        {
            var category = _repo.GetById(id);

            if (category == null)
                throw new Exception("Loại sản phẩm không tồn tại!");

            if (category.Products.Any())
                throw new Exception("Không thể xóa vì đang có sản phẩm thuộc loại này!");

            _repo.Delete(category);
        }
    }
}
