using Microsoft.AspNetCore.Mvc;
using SalesApp.DTOs;
using SalesApp.Services;

namespace SalesApp.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryService _service;

        public CategoryController(CategoryService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var categories = _service.GetAll();
            return View(categories);
        }

 

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateCategoryDTO dto)
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
            var category = _service.Get(id);

            if (category == null)
                return NotFound();

            var dto = new UpdateCategoryDTO
            {
                CategoryId = category.CategoryId,
                Name = category.Name
            };

            return View(dto);
        }

        [HttpPost]
        public IActionResult Edit(UpdateCategoryDTO dto)
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