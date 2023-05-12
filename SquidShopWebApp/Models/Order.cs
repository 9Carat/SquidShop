using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SquidShopWebApp.Models
{
	public class Order
	{
        public int OrderId { get; set; }
        public int MyProperty { get; set; }
        public string FK_UserId { get; set; }
        [StringLength(25)]
        public string OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        [StringLength(50)]
        public string ShippingAddress { get; set; }
        public User User { get; set; }
        public ICollection<OrderList> OrderLists { get; set; }
    }
}
