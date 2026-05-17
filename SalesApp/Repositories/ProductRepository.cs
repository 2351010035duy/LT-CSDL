using Microsoft.EntityFrameworkCore;
using SalesApp.Data;
using SalesApp.Models;

namespace SalesApp.Repositories
{
    public class ProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Product> GetAll()
        {
            return _context.Products
                .Include(p => p.Category)
                .ToList();
        }

        public Product GetById(int id)
        {
            return _context.Products.Include(p => p.Category).FirstOrDefault(p => p.ProductId == id);
        }

        public List<Product> GetByCategory(int categoryId)
        {
            return _context.Products
                .Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId)
                .ToList();
        }
        public void Add(Product p)
        {
            _context.Products.Add(p);
            _context.SaveChanges();
        }

        public void Update(Product p)
        {
            //_context.Products.Update(p);
            //_context.SaveChanges();
            var trackedEntity = _context.Products.Local.FirstOrDefault(x => x.ProductId == p.ProductId);
            if (trackedEntity != null)
            {
                _context.Entry(trackedEntity).State = EntityState.Detached;
            }
            _context.Products.Update(p);
            _context.SaveChanges();

        }

        public void Delete(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
        public bool IsProductUsed(int productId)
        {
            return _context.OrderDetails.Any(od => od.ProductId == productId);
        }
    }
}