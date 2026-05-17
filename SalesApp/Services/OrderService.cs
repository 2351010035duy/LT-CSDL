using SalesApp.DTOs;
using SalesApp.Models;
using SalesApp.Repositories;

namespace SalesApp.Services
{
    public class OrderService
    {
        private readonly OrderRepository _repo;
        private readonly ProductRepository _productRepo;

        public OrderService(OrderRepository repo, ProductRepository productRepo)
        {
            _repo = repo;
            _productRepo = productRepo;
        }
        public List<OrderDTO> GetAll()
        {
            return _repo.GetAll().Select(o => new OrderDTO
            {
                OrderId = o.OrderId,
                CustomerName = o.Customer.Name,
                OrderDate = o.OrderDate,
                Total = o.OrderDetails.Sum(x => x.Price * x.Quantity),
                Products = o.OrderDetails
                    .Select(od => od.Product.Name)
                    .ToList()
            }).ToList();
        }
        public OrderDTO Get(int id)
        {
            var o = _repo.GetById(id);

            if (o == null)
                throw new Exception("Hóa đơn không tồn tại!");
            var total = o.OrderDetails.Sum(x => x.Price * x.Quantity);

            return new OrderDTO
            {
                OrderId = o.OrderId,
                CustomerName = o.Customer.Name,
                OrderDate = o.OrderDate,
                Total = total,
                Products = o.OrderDetails
                    .Select(od => od.Product.Name)
                    .ToList()
            };
        }

        public void Create(CreateOrderDTO dto)
        {
            if (dto.Items == null || !dto.Items.Any())
                throw new Exception("Hóa đơn phải có ít nhất 1 sản phẩm!");

            var order = new Order
            {
                CustomerId = dto.CustomerId,
                OrderDate = DateTime.Now,
                OrderDetails = new List<OrderDetail>()
            };

            foreach (var item in dto.Items)
            {
                var product = _productRepo.GetById(item.ProductId);

                if (product == null)
                    throw new Exception($"Sản phẩm {item.ProductId} không tồn tại!");

                order.OrderDetails.Add(new OrderDetail
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = product.Price
                });
            }

            _repo.Add(order);
        }

        public void Delete(int id)
        {
            var order = _repo.GetById(id);

            if (order == null)
                throw new Exception("Hóa đơn không tồn tại!");

            _repo.Delete(order);
        }
        public void Update(int id, UpdateOrderDTO dto)
        {
            var order = _repo.GetById(id);

            if (order == null)
                throw new Exception("Hóa đơn không tồn tại!");

            order.CustomerId = dto.CustomerId;

            order.OrderDetails.Clear();

            foreach (var item in dto.Items)
            {
                var product = _productRepo.GetById(item.ProductId);

                if (product == null)
                    throw new Exception($"Sản phẩm {item.ProductId} không tồn tại!");

                order.OrderDetails.Add(new OrderDetail
                {
                    OrderId = order.OrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = product.Price
                });
            }

            _repo.Update(order);
        }

    }
}
