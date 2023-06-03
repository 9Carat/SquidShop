using Microsoft.AspNetCore.Mvc;

namespace SquidShopWebApp.Models
{
    public class OrderViewModel
    {
        public Order Order { get; set; }
        public List<OrderList> OrderList { get; set; }
        public List<Product> Product { get; set; }
        public string ApiResponse { get; set; }
    }
}
