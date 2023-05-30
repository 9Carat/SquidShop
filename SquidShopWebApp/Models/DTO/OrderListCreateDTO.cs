namespace SquidShopWebApp.Models.DTO
{
	public class OrderListCreateDTO
	{
		public double Price { get; set; }
		public int Quantity { get; set; }
		public int FK_ProductId { get; set; }
		public int FK_OrderId { get; set; }
	}
}
