using ErpCrm.Domain.Entities;

namespace ErpCrm.Application.Common.Interfaces;

public interface IJwtService
{
    string GenerateAccessToken(
        User user,
        IList<string> roles);

    string GenerateRefreshToken();
    DateTime GetAccessTokenExpiryDate();
    DateTime GetRefreshTokenExpiryDate();
}