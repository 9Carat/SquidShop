using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SquidShopWebApp.Models
{
	public class Order
	{
        public int OrderId { get; set; }
        public int FK_UserId { get; set; }
        [StringLength(25)]
        public bool OrderStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLength(50)]
        public string ShippingAddress { get; set; }
        public User User { get; set; }
        public ICollection<OrderList> OrderLists { get; set; }
    }
}
