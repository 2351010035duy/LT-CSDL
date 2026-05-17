using SalesApp.DTOs;
using SalesApp.Models;
using SalesApp.Repositories;

namespace SalesApp.Services
{
    public class CustomerService
    {
        private readonly CustomerRepository _repo;

        public CustomerService(CustomerRepository repo)
        {
            _repo = repo;
        }
        public List<CustomerDTO> GetAll()
        {
            return _repo.GetAll().Select(c => new CustomerDTO
            {
                CustomerId = c.CustomerId,
                Name = c.Name,
                Phone = c.Phone,
                Address = c.Address,
                Orders = c.Orders.Select(o => $"Order {o.OrderId}").ToList()
            }).ToList();
        }
        public CustomerDTO Get(int id)
        {
            var c = _repo.GetById(id);

            if (c == null)
                throw new Exception("Người dùng không tồn tại!");

            return new CustomerDTO
            {
                CustomerId = c.CustomerId,
                Name = c.Name,
                Phone = c.Phone,
                Address = c.Address,
                Orders = c.Orders.Select(o => $"Order {o.OrderId}").ToList()
            };
        }
        public void Create(CreateCustomerDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Name))
                throw new Exception("Tên không được thiếu!");

            var customer = new Customer
            {
                Name = dto.Name,
                Address = dto.Address,
                Phone = dto.Phone,
                Orders = new List<Order>()
            };

            _repo.Add(customer);
        }
        public void Update(UpdateCustomerDTO dto)
        {
            var customer = _repo.GetById(dto.CustomerId);

            if (customer == null)
                throw new Exception("Người dùng không tồn tại!");

            customer.Name = dto.Name;
            customer.Phone = dto.Phone;
            customer.Address = dto.Address;

            _repo.Update(customer);
        }
        public void Delete(int id)
        {
            var customer = _repo.GetById(id);

            if (customer == null)
                throw new Exception("Người dùng không tồn tại!");

            _repo.Delete(customer);
        }
    }
}
