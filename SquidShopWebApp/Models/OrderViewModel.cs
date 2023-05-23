using Microsoft.AspNetCore.Mvc;

namespace SquidShopWebApp.Models
{
    public class OrderViewModel
    {
        public Order Order { get; set; }
        public OrderList OrderList { get; set; }
        public Product Product { get; set; }
        public string ApiResponse { get; set; }
    }
}
