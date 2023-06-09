using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SquidShopWebApp.Data;
using SquidShopWebApp.Services;
using SquidShopWebApp.Services.IServices;

namespace SquidShopWebApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
			builder.Services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(connectionString));
			builder.Services.AddDatabaseDeveloperPageExceptionFilter();

			builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
				.AddRoles<IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>();
			builder.Services.AddControllersWithViews();
			builder.Services.AddAutoMapper(typeof(MappingConfig));
			builder.Services.AddHttpClient<IProductService, ProductService>();
			builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddHttpClient<IOrderService, OrderService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddHttpClient<IOrderListService, OrderListService>();
            builder.Services.AddScoped<IOrderListService, OrderListService>();
            builder.Services.AddHttpClient<IUserService, UserService>();
            builder.Services.AddScoped<IUserService, UserService>();
			builder.Services.AddHttpClient<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();

            var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseMigrationsEndPoint();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");
			app.MapRazorPages();

			app.Run();
		}
	}
}