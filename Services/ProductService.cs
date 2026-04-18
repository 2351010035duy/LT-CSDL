using Microsoft.EntityFrameworkCore;
using SalesApp.DTOs;
using SalesApp.Models;
using SalesApp.Repositories;

namespace SalesApp.Services
{
    public class ProductService
    {
        private readonly ProductRepository _repo;

        public ProductService(ProductRepository repo)
        {
            _repo = repo;
        }

        public List<ProductDTO> GetAll()
        {
            var products = _repo.GetAll();

            return products.Select(p => new ProductDTO
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                CategoryName = p.Category.Name
            }).ToList();
        }

        public ProductDTO Get(int id)
        {
            var p = _repo.GetById(id);

            if (p == null)
                throw new Exception("Sản phẩm không tồn tại!");

            return new ProductDTO
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                CategoryName = p.Category?.Name
            };
        }

        public void Create(CreateProductDTO dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Stock = dto.Stock,
                CategoryId = dto.CategoryId
            };

            _repo.Add(product);
        }

        public void Update(UpdateProductDTO dto)
        {
            var product = new Product
            {
                ProductId = dto.ProductId,
                Name = dto.Name,
                Price = dto.Price,
                Stock = dto.Stock,
                CategoryId = dto.CategoryId
            };

            _repo.Update(product);
        }

        public void Delete(int id)
        {
            var product = _repo.GetById(id);

            if (product == null)
                throw new Exception("Sản phẩm không tồn tại!");

            if (_repo.IsProductUsed(id))
                throw new Exception("Không thể xóa vì sản phẩm đã có trong hóa đơn!");

            _repo.Delete(product);
        }
        public List<ProductDTO> GetByCategory(int categoryId)
        {
            return _repo.GetByCategory(categoryId)
                .Select(p => new ProductDTO
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Price = p.Price,
                    Stock = p.Stock,
                    CategoryName = p.Category.Name
                })
                .ToList();
        }
    }
}