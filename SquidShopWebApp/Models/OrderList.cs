namespace SquidShopWebApp.Models
{
	public class OrderList
	{
        public int OrderListId { get; set; }
        public int FK_ProductId { get; set; }
        public int Fk_OrderId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
