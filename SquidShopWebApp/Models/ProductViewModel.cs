namespace SquidShopWebApp.Models
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public int? Quantity { get; set; }
        public string? ShippingAddress { get; set; }
        public int UserId { get; set; }
    }
}
