using Microsoft.AspNetCore.Http.Features;

namespace SquidShopWebApp.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public string UserId { get; set; } 
        public ICollection<CartItem> CartItems { get; set; }
    }
}
