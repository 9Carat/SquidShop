using System.ComponentModel.DataAnnotations.Schema;

namespace SquidShopWebApp.Models.DTO
{
	public class OrderCreateDTO
	{
		public int FK_UserId { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.Now;
		public bool OrderStatus { get; set; }
	}
}
