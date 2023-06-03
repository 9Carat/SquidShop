using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SquidShopWebApp.Models
{
	public class Category
	{
        public int CategoryId { get; set; }
        [StringLength(25)]
        [DisplayName("Category")]
        public string CategoryName { get; set; }
        [StringLength(40)]
        public string Details { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
