namespace SquidShopWebApp.Models
{
    public class Promotion
    {
        public int PromotionId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double DiscountPercentage { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
