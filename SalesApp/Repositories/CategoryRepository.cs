using Microsoft.EntityFrameworkCore;
using SalesApp.Data;
using SalesApp.Models;

namespace SalesApp.Repositories
{
    public class CategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Category> GetAll()
        {
            return _context.Categories
                .Include(c => c.Products)
                .ToList();
        }
        public Category GetById(int id)
        {
            return _context.Categories.Include(c => c.Products).FirstOrDefault(c => c.CategoryId == id);
        }
        public void Add(Category c)
        {
            _context.Categories.Add(c);
            _context.SaveChanges();
        }
        public void Update(Category c)
        {
            _context.Categories.Update(c);
            _context.SaveChanges();
        }
        public void Delete(Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
}
