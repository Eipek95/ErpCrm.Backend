namespace ErpCrm.Application.Features.ProductImages.DTOs;

public class ProductImageDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int? ProductVariantId { get; set; }
    public string ImageUrl { get; set; } = null!;
    public string? AltText { get; set; }
    public bool IsMain { get; set; }
    public int SortOrder { get; set; }
}
