using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SquidShopWebApp.Models;

namespace SquidShopWebApp.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
		public DbSet<Product> Products { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderList> OrderLists { get; set; }
		public DbSet<Category> Categories { get; set; }
    public DbSet<Promotion> Promotions { get; set; }
		public DbSet<Customer> Customers { get; set; }
	}
}