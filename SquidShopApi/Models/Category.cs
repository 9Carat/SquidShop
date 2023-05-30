using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SquidShopApi.Models
{
	public class Category
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CategoryId { get; set; }
		[Required]
		[StringLength(50)]
		public string CategoryName { get; set; }
        [StringLength(200)]
        public string Details { get; set; }
		public ICollection<Product> Products { get; set; } //nav
	}
}
