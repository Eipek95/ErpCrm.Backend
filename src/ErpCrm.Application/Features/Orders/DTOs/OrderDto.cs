namespace ErpCrm.Application.Features.Orders.DTOs;

public class OrderDto
{
    public int Id { get; set; }
    public string OrderNumber { get; set; } = null!;
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = null!;
    public int CreatedByUserId { get; set; }
    public string CreatedByUserName { get; set; } = null!;
    public int Status { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime CreatedDate { get; set; }
    public List<OrderItemDto> Items { get; set; } = new();
}
