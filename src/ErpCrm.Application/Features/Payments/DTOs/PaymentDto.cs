namespace ErpCrm.Application.Features.Payments.DTOs;

public class PaymentDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string OrderNumber { get; set; } = null!;
    public int Status { get; set; }
    public int Method { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaidDate { get; set; }
    public DateTime CreatedDate { get; set; }
}
