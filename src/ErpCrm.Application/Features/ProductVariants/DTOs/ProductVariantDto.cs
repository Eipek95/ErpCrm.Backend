namespace ErpCrm.Application.Features.ProductVariants.DTOs;

public class ProductVariantDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public string VariantCode { get; set; } = null!;
    public string? Color { get; set; }
    public string? Size { get; set; }
    public decimal? Price { get; set; }
    public int StockQuantity { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
}
