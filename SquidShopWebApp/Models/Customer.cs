using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SquidShopWebApp.Models
{
	public class Customer : IdentityUser
	{
        [StringLength(25)]
        public string FirstName { get; set; }
        [StringLength(25)]
        public string LastName { get; set; }
        [StringLength(25)]
        public string Address { get; set; }
        [StringLength(6)]
        public string PostalCode { get; set; }
        [StringLength(15)]
        public string City { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
