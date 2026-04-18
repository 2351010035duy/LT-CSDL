using System.ComponentModel.DataAnnotations;

public class CreateOrderDTO
{
    [Required]
    public int CustomerId { get; set; }

    [Required]
    public List<OrderItemDTO> Items { get; set; } = new();
}

public class OrderItemDTO
{
    [Required]
    public int ProductId { get; set; }

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}
