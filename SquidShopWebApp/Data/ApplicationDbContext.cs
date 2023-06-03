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
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        //public DbSet<Product> Products { get; set; }
        //public DbSet<Order> Orders { get; set; }
        //public DbSet<OrderList> OrderLists { get; set; }
        //public DbSet<Category> Categories { get; set; }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //	builder.Entity<Category>()
        //		.HasData(
        //			new Category()
        //			{
        //				CategoryId = 1,
        //				Name = "Food",
        //				Details = "Food from all over the world",
        //			});

        //	builder.Entity<Product>()
        //		.HasOne(p => p.Categories)
        //		.WithMany(c => c.Products)
        //		.HasForeignKey(p => p.FK_CategoryId);

        //	builder.Entity<OrderList>()
        //		.HasOne(ol => ol.Product)
        //		.WithMany(p => p.OrderLists)
        //		.HasForeignKey(ol => ol.FK_ProductId);

        //	builder.Entity<OrderList>()
        //		.HasOne(ol => ol.Order)
        //		.WithMany(o => o.OrderLists)
        //		.HasForeignKey(ol => ol.Fk_OrderId);

        //	builder.Entity<Order>()
        //		.HasOne(o => o.User)
        //		.WithMany(p => p.Orders)
        //		.HasForeignKey(o => o.FK_UserId);

        //	base.OnModelCreating(builder);
        //}
    }
}