using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SalesApp.DTOs;
using SalesApp.Services;
namespace SalesApp.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;
        public ProductController(ProductService productService, CategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        //public IActionResult Index(int id)
        //{
        //    var product = _productService.Get(id);
        //    return View(product);
        //}
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = GetCategories();

            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateProductDTO dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(
                    _categoryService.GetAll(),
                    "CategoryId",
                    "Name"
                );

                return View(dto);
            }

            _productService.Create(dto);

            return RedirectToAction("Index", "Home");
        }
        private List<SelectListItem> GetCategories()
        {
            return _categoryService.GetAll()
                .Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.Name
                })
                .ToList();
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            
            _productService.Delete(id);

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Index(int id)
        {
            var product = _productService.GetEntity(id);

            if (product == null)
            {
                return NotFound();
            }

            var dto = new UpdateProductDTO
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId
            };

            ViewBag.Categories = new SelectList(
                _categoryService.GetAll(),
                "CategoryId",
                "Name",
                dto.CategoryId
            );

            return View(dto);
        }
        [HttpPost]
        public IActionResult Index(UpdateProductDTO dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(
                    _categoryService.GetAll(),
                    "CategoryId",
                    "Name",
                    dto.CategoryId
                );

                return View(dto);
            }

            var product = _productService.Get(dto.ProductId);
            if (product == null)
            {
                return NotFound();
            }

            _productService.Update(dto);

            return RedirectToAction("Index", "Home");
        }
    }
}
