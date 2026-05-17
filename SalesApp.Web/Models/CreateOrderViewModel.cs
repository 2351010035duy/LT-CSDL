using Microsoft.AspNetCore.Mvc.Rendering;
using SalesApp.DTOs;
namespace SalesApp.DTOs
{
    public class CreateOrderViewModel
    {
        public int CustomerId { get; set; }

        public List<OrderItemDTO> Items { get; set; }
            = new List<OrderItemDTO>();

        public List<SelectListItem> Customers { get; set; }
            = new List<SelectListItem>();

        public List<SelectListItem> Products { get; set; }
            = new List<SelectListItem>();
    }
}
