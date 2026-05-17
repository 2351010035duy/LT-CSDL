using Microsoft.EntityFrameworkCore;
using SalesApp.Data;
using SalesApp.Models;

namespace SalesApp.Repositories
{
    public class CustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }
        public List<Customer> GetAll()
        {
            return _context.Customers
                .Include(c => c.Orders)
                .ToList();
        }

        public Customer GetById(int id)
        {
            return _context.Customers
                .Include(c => c.Orders)
                .FirstOrDefault(c => c.CustomerId == id);
        }
        public void Add(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }
        public void Update(Customer customer)
        {
            _context.Customers.Update(customer);
            _context.SaveChanges();
        }
        public void Delete(Customer customer)
        {
            _context.OrderDetails.RemoveRange(
                _context.OrderDetails.Where(od => od.Order.CustomerId == customer.CustomerId)
            );

            _context.Orders.RemoveRange(customer.Orders);

            _context.Customers.Remove(customer);

            _context.SaveChanges();
        }
    }
}
