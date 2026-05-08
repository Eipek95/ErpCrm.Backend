using ErpCrm.Domain.Common;

namespace ErpCrm.Domain.Entities
{
    public class AuditLog : BaseEntity
    {
        public int? UserId { get; set; }
        public User? User { get; set; }

        public string Action { get; set; } = null!;
        public string EntityName { get; set; } = null!;

        public string? OldValues { get; set; }
        public string? NewValues { get; set; }

        public string? IPAddress { get; set; }
    }
}
