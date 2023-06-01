using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SquidShopWebApp.Models
{
    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartItemId { get; set; }
        [Required]
        [ForeignKey("Cart")]
        public int Fk_CartId { get; set; }
        public Cart Cart { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        [Range(0,10000)]
        public double UnitPrice { get; set; }
        [Range(0,100)]
        public decimal Discount { get; set; }
        [Range(0,10000)]
        public double DiscountUnitPrice { get; set; }
        [StringLength(75)]
        [DisplayName("Image Name")]
        public string ImageName { get; set; }
        [Required]
        [ForeignKey("Product")]
        public int Fk_ProductId { get; set; }
        public Product Product { get; set; }
        [Required]
        [StringLength(75)]
        public string ProductName { get; set; }
    }
}
