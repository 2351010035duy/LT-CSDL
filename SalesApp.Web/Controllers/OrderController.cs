using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SalesApp.DTOs;
using SalesApp.Services;

namespace SalesApp.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService _service;
        private readonly CustomerService _customerService;
        private readonly ProductService _productService;

        public OrderController(
        OrderService service,
        CustomerService customerService,
        ProductService productService)
        {
            _service = service;
            _customerService = customerService;
            _productService = productService;
        }

        public IActionResult Index()
        {
            var orders = _service.GetAll();
            return View(orders);
        }

        public IActionResult Details(int id)
        {
            var order = _service.Get(id);
            return View(order);
        }

        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Create()
        {
            var vm = new CreateOrderViewModel();

            vm.Customers = _customerService.GetAll()
                .Select(c => new SelectListItem
                {
                    Value = c.CustomerId.ToString(),
                    Text = c.Name
                }).ToList();

            vm.Products = _productService.GetAll()
                .Select(p => new SelectListItem
                {
                    Value = p.ProductId.ToString(),
                    Text = $"{p.Name} - {p.Price}"
                }).ToList();

            vm.Items.Add(new OrderItemDTO());

            return View(vm);
        }
        [HttpPost]
        public IActionResult Create(CreateOrderViewModel vm)
        {
            var dto = new CreateOrderDTO
            {
                CustomerId = vm.CustomerId,
                Items = vm.Items
            };

            _service.Create(dto);

            return RedirectToAction("Index");
        }
    }
}
