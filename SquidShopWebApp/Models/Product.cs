using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SquidShopWebApp.Models
{
	public class Product
	{
        public int ProductId { get; set; }
        [StringLength(25)]
        [DisplayName("Product name")]
        public string ProductName { get; set; }
        [DisplayName("In stock")]
        public int InStock { get; set; }
        [DisplayName("Unit price")]
        public double UnitPrice { get; set; }
        public decimal Discount { get; set; }
        [DisplayName("Price after discount")]
        public double DiscountUnitPrice { get; set; }
        [StringLength(75)]
        [DisplayName("Image name")]
        public string ImageName { get; set; }
        [NotMapped]
        [DisplayName("Upload Image")]
        public IFormFile ImageFile { get; set; }
        [DisplayName("Category")]
        public int FK_CategoryId { get; set; }
        public Category Categories { get; set; }
        public ICollection<OrderList> OrderLists { get; set; }
    }
}
