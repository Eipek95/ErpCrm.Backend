namespace ErpCrm.Application.Features.AuditLogs.DTOs;
public class AuditLogDto
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public string Action { get; set; } = null!;
    public string EntityName { get; set; } = null!;
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }
    public string? IPAddress { get; set; }
    public DateTime CreatedDate { get; set; }
}
