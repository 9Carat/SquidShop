using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SquidShopApi.Models
{
	public class Product
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ProductId { get; set; }
		[Required]
		[StringLength(50)]
		public string ProductName { get; set; }
		[Required]
		public int InStock { get; set; }
		[Required]
		public double UnitPrice { get; set; }
		public decimal Discount { get; set; } = 0;
		public double DiscountUnitPrice { get; set; } = 0;
		public string IMG { get; set; }
		[ForeignKey("Categories")]
		public int FK_CategoryId { get; set; }
		public Category Categories { get; set; } //nav
		public virtual ICollection<OrderList> OrderLists { get; set; }//nav
		
	}
}
