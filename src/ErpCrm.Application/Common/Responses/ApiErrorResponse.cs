using System;
using System.Collections.Generic;
using System.Text;

namespace ErpCrm.Application.Common.Responses
{
    public class ApiErrorResponse
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; } = "An unexpected error occurred.";
        public List<string> Errors { get; set; } = new();
        public int StatusCode { get; set; }
        public string? TraceId { get; set; }
    }
}
