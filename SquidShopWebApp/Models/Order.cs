using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SquidShopWebApp.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int FK_UserId { get; set; }
        [StringLength(25)]
        public bool OrderStatus { get; set; }
        [DisplayName("Created At")]
        public DateTime CreatedAt { get; set; }
        [StringLength(50)]
        [DisplayName("Shipping Address")]
        public string ShippingAddress { get; set; }
        public User Customer { get; set; }
        public ICollection<OrderList> OrderLists { get; set; }
    }
}