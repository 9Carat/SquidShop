using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SquidShopWebApp.Models.DTO
{
	public class ProductUpdateDTO
	{
		public int ProductId { get; set; }
		[StringLength(25)]
		public string ProductName { get; set; }
		public int InStock { get; set; }
		public double UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public double DiscountUnitPrice { get; set; }
		[StringLength(75)]
		[DisplayName("Image Name")]
		public string ImageName { get; set; }
		//[NotMapped]
		//[DisplayName("Upload Image")]
		//public IFormFile ImageFile { get; set; }
		public int FK_CategoryId { get; set; }
	}
}
