using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneStopShop.API.Models
{
    public class ResponseDTO
    {
        public string Message { get; set; }

        public bool Success { get; set; }

        public string ErrorMessage { get; set; }
    }
}
