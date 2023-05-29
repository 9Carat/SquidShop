using SquidShopApi.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SquidShopApi.Models
{
    public class Promotion
    {
        public int PromotionId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double DiscountProcent { get; set; }
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [NotMapped]
        public string ProductName => Product?.ProductName;
    }
}
