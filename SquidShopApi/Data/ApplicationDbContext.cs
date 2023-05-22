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

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Product>().HasData(
				new Product()
				{
					ProductId = 1,
					ProductName = "Jonny Boy",
					InStock = 10,
					UnitPrice = 199,
					IMG = "No URL",
					FK_CategoryId = 1,
				},
				new Product()
				{
					ProductId = 2,
					ProductName = "After the laughter comes tears",
					InStock = 29,
					UnitPrice = 149,
					IMG = "No URL",
					FK_CategoryId = 1,
				});
			modelBuilder.Entity<Category>().HasData(
				new Category()
				{
					CategoryId = 1,
					CategoryName = "Toys",
					Details = "Play things"
				},
				new Category()
				{
					CategoryId = 2,
					CategoryName = "Tools",
					Details = "Work things"
				});
			modelBuilder.Entity<User>().HasData(
				new User()
				{
					UserId = 1,
					FirstName = "Sven",
					LastName = "Knutsson",
					Address = "Forest",
					PostalCode = "20211",
					City = "There"
				});
		}
	}
}
