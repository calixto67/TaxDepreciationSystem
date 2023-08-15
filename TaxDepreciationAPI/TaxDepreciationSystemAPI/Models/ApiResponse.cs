using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BMTTaxDepreciation.Backend.Api.Common.Enums;

namespace BMTTaxDepreciation.Backend.Api.Models
{
    public class ApiResponse
    {
        public string Message { get; set; }

        public Object Data { get; set; }

        public string Status { get; set; }
    }
}
