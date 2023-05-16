namespace SquidShopApi.Models.DTO
{
	public class OrderListUpdateDTO
	{
		public int OrderListId { get; set; }
		public double Price { get; set; }
		public int Quantity { get; set; }
		public int FK_ProductId { get; set; }
		public int FK_OrderId { get; set; }
	}
}
