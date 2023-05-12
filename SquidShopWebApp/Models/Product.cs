using System.ComponentModel.DataAnnotations;

namespace SquidShopWebApp.Models
{
	public class Product
	{
        public int ProductId { get; set; }
        [StringLength(25)]
        public string ProductName { get; set; }
        public int Stock { get; set; }
        public double UnitPrice { get; set; }
        public bool Discount { get; set; }
        public double DiscountPrice { get; set; }
        [StringLength(75)]
        public string ImgURL { get; set; }
        public int FK_CategoryId { get; set; }
        public Category Categories { get; set; }
        public ICollection<OrderList> OrderLists { get; set; }
    }
}
