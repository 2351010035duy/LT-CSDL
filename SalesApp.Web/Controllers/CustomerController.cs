using Microsoft.AspNetCore.Mvc;
using SalesApp.DTOs;
using SalesApp.Services;

namespace SalesApp.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly CustomerService _service;

        public CustomerController(CustomerService service)
        {
            _service = service;
        }


        public IActionResult Index()
        {
            var customers = _service.GetAll();

            return View(customers);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateCustomerDTO dto)
        {
            try
            {
                _service.Create(dto);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;

                return View(dto);
            }
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var customer = _service.Get(id);

            var dto = new UpdateCustomerDTO
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                Phone = customer.Phone,
                Address = customer.Address
            };

            return View(dto);
        }

        [HttpPost]
        public IActionResult Edit(UpdateCustomerDTO dto)
        {
            try
            {
                _service.Update(dto);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;

                return View(dto);
            }
        }


        public IActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("Index");
        }
    }
}
