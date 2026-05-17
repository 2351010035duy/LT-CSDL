using Microsoft.AspNetCore.Mvc;
using SalesApp.DTOs;
using SalesApp.Services;

namespace SalesApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _service;

        public ProductsController(ProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get() => Ok(_service.GetAll());

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                _service.Get(id);
                    return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Create(CreateProductDTO dto)
        {
            _service.Create(dto);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(UpdateProductDTO dto)
        {
            _service.Update(dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return Ok(new { message = "Đã xóa thành công!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("by-category/{id}")]
        public IActionResult GetByCategory(int id)
        {
            return Ok(_service.GetByCategory(id));
        }
    }
}