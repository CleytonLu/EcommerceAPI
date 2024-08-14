using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;

namespace EcommerceAPI.Models
{
    public class Order
    {
        public int id { get; set; }
        public int client_id { get; set; }
        public int seller_id { get; set; }
        public string delivery_type { get; set; }
        public string order_status { get; set; }
        public decimal total_price { get; set; }
        [NotMapped]
        public string? client_name { get; set; }
        [NotMapped]
        public string? seller_name { get; set; }
        [NotMapped]
        public IEnumerable<OrderItems>? order_items { get; set; }
    }
}