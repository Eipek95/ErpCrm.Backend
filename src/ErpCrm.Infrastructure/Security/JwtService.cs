using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ErpCrm.Infrastructure.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateAccessToken(
        User user,
        IList<string> roles)
    {
        var jwtSettings = _configuration.GetSection("Jwt");

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.FullName),
            new(ClaimTypes.Email, user.Email)
        };

        foreach (var role in roles)
        {
            claims.Add(
                new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                jwtSettings["SecretKey"]!));

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var expires = GetAccessTokenExpiryDate();

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: expires,
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomBytes = new byte[64];

        using var rng = RandomNumberGenerator.Create();

        rng.GetBytes(randomBytes);

        return Convert.ToBase64String(randomBytes);
    }

    public DateTime GetAccessTokenExpiryDate()
    {
        var minutes = Convert.ToDouble(
            _configuration["Jwt:AccessTokenExpirationMinutes"]);

        return DateTime.UtcNow.AddMinutes(minutes);
    }

    public DateTime GetRefreshTokenExpiryDate()
    {
        var days = Convert.ToDouble(
            _configuration["Jwt:RefreshTokenExpirationDays"]);

        return DateTime.UtcNow.AddDays(days);
    }
}