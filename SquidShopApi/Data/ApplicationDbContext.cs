using Microsoft.EntityFrameworkCore;
using SquidShopApi.Models;

namespace SquidShopApi.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{

		}
		public DbSet<Category> Categories { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderList> OrderLists { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<User> Users { get; set; }
    public DbSet<Promotion> Promotions { get; set; }
    }
}
