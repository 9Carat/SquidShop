using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SquidShopWebApp.Models
{
	public class Order
	{
        public int OrderId { get; set; }
        [ForeignKey("Users")]
        public int FK_UserId { get; set; }
        public User Users { get; set; }//nav
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool OrderStatus { get; set; }
        public virtual ICollection<OrderList> OrderLists { get; set; }//nav
    }
}
