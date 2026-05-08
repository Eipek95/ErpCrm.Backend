namespace ErpCrm.Application.Features.Warehouses.DTOs;
public class WarehouseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string City { get; set; } = null!;
    public string? Address { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
}
