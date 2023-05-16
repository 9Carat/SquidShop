using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SquidShopApi.Models
{
	public class OrderList
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int OrderListId { get; set; }
		public double Price { get; set; }
		public int Quantity { get; set; }
		[ForeignKey("Products")]
		[DisplayName("Product")]
		public int FK_ProductId { get; set; }
		public Product Products { get; set; }//nav
		[ForeignKey("Orders")]
		public int FK_OrderId { get; set; }
		public Order Orders { get; set; }//nav   
	}
}
