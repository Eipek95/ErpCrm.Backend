using ErpCrm.Domain.Common;
using ErpCrm.Domain.Entities;

public class Customer : BaseEntity
{
    public string CompanyName { get; set; } = null!;
    public string? ContactName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }

    public string City { get; set; } = null!;
    public string? District { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
