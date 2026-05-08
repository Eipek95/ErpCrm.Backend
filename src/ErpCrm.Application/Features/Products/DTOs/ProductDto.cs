namespace ErpCrm.Application.Features.Products.DTOs;
public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string SKU { get; set; } = null!;
    public decimal Price { get; set; }
    public decimal CostPrice { get; set; }
    public bool IsPopular { get; set; }
    public bool IsActive { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
}
