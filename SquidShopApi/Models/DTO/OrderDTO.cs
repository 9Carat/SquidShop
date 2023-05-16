using System.ComponentModel.DataAnnotations.Schema;

namespace SquidShopApi.Models.DTO
{
	public class OrderDTO
	{
		public int OrderId { get; set; }
		public int FK_UserId { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.Now;
		public bool OrderStatus { get; set; }
	}
}
