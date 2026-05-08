using System;
using System.Collections.Generic;
using System.Text;

namespace ErpCrm.Application.Common.Models
{
    public class CurrentUserModel
    {
        public int UserId { get; set; }
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public List<string> Roles { get; set; } = new();
    }
}
