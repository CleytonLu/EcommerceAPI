namespace EcommerceAPI.DTOs
{
    public class CreateOrderDTO
    {
        public int client_id { get; set; }
        public int seller_id { get; set; }
        public string delivery_type { get; set; }
        public string order_status { get; set; }
        public decimal total_price { get; set; }
    }

    public class UpdateOrderDTO
    {
        public string order_status { get; set; }
    }
}
