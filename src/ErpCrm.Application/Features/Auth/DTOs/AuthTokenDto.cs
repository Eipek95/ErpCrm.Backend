using System;
using System.Collections.Generic;
using System.Text;

namespace ErpCrm.Application.Features.Auth.DTOs
{
    public class AuthTokenDto
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public DateTime AccessTokenExpiresAt { get; set; }
        public DateTime RefreshTokenExpiresAt { get; set; }
    }
}
