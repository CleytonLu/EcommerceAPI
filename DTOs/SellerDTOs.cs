using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceAPI.DTOs
{
    public class CreateSellerDTO
    {
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string? phone { get; set; }
        public string  reference { get; set; }
    }

    public class UpdateSellerDTO
    {
        public string name { get; set; }
        public string password { get; set; }
        public string? phone { get; set; }
        public string  reference { get; set; }
    }
}