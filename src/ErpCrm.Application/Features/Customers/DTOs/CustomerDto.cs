using System;
using System.Collections.Generic;
using System.Text;

namespace ErpCrm.Application.Features.Customers.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = null!;
        public string? ContactName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string City { get; set; } = null!;
        public string? District { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
