using Microsoft.AspNetCore.Identity;

namespace SquidShopWebApp.Models
{
	public class User : IdentityUser
	{
        public ICollection<Order> Orders { get; set; }
    }
}
