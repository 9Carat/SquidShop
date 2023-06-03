namespace SquidShopWebApp.Models.DTO
{
    public class PromotionCreateDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double DiscountProcent { get; set; }
        public int ProductId { get; set; }
        public List<Product> Products { get; set; }
    }
}
