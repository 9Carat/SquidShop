namespace SquidShopWebApp.Models.DTO
{
    public class OrderUpdateDTO
    {
        public int OrderId { get; set; }
        public int FK_UserId { get; set; }
        public bool OrderStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ShippingAddress { get; set; }
    }
}
