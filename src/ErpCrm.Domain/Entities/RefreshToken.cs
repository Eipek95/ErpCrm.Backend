using ErpCrm.Domain.Common;

namespace ErpCrm.Domain.Entities;

public class RefreshToken : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public string Token { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public DateTime? RevokedAt { get; set; }

    public string? ReplacedByToken { get; set; }

    public string? CreatedByIp { get; set; }

    public string? RevokedByIp { get; set; }

    public bool IsRevoked => RevokedAt.HasValue;

    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;

    public bool IsActive => !IsRevoked && !IsExpired;
}