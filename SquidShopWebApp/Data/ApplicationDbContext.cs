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


        protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Promotion>()
				.HasOne(p => p.Product)
				.WithOne(pr => pr.Promotion)
				.HasForeignKey<Promotion>(p => p.ProductId);
		

			builder.Entity<Category>()
				.HasData(
					new Category()
					{
						CategoryId = 1,
						Name = "Food",
						Details = "Food from all over the world",
					});

			builder.Entity<Product>()
				.HasOne(p => p.Categories)
				.WithMany(c => c.Products)
				.HasForeignKey(p => p.FK_CategoryId);

			builder.Entity<OrderList>()
				.HasOne(ol => ol.Product)
				.WithMany(p => p.OrderLists)
				.HasForeignKey(ol => ol.FK_ProductId);

			builder.Entity<OrderList>()
				.HasOne(ol => ol.Order)
				.WithMany(o => o.OrderLists)
				.HasForeignKey(ol => ol.Fk_OrderId);

			builder.Entity<Order>()
				.HasOne(o => o.User)
				.WithMany(p => p.Orders)
				.HasForeignKey(o => o.FK_UserId);

			base.OnModelCreating(builder);
		}
	}
}