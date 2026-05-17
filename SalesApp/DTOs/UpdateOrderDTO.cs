namespace SalesApp.DTOs
{
    public class UpdateOrderDTO
    {
        public int CustomerId { get; set; }

        public List<OrderItemDTO> Items { get; set; }
    }
}
