using Microsoft.AspNetCore.Identity;

namespace SquidShopWebApp.Models
{
    public class AdminViewModel
    {
        public IdentityUser[] Admin { get; set; }
        public IdentityUser[] Customer { get; set; }

    }
}
