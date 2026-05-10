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

        public string? IpAddress { get; set; }

        // Yeni alanlar
        public string? UserAgent { get; set; }

        public string? Endpoint { get; set; }

        public string? HttpMethod { get; set; }

        public string? CorrelationId { get; set; }

        public long? ExecutionTimeMs { get; set; }
    }
}
