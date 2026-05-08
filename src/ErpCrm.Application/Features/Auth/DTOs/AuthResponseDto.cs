using System;
using System.Collections.Generic;
using System.Text;

namespace ErpCrm.Application.Features.Auth.DTOs
{
    public class AuthResponseDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public List<string> Roles { get; set; } = new();

        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public DateTime RefreshTokenExpiresAt { get; set; }
    }
}
