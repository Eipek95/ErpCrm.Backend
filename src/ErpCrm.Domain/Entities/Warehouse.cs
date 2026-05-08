using ErpCrm.Domain.Common;

namespace ErpCrm.Domain.Entities
{
    public class Warehouse : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string City { get; set; } = null!;
        public string? Address { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<Stock> Stocks { get; set; } = new List<Stock>();
        public ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();
    }
}
