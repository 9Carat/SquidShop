using System.ComponentModel.DataAnnotations;

namespace SquidShopWebApp.Models
{
	public class Category
	{
        public int CategoryId { get; set; }
        [StringLength(25)]
        public string Name { get; set; }
        [StringLength(40)]
        public string Details { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
