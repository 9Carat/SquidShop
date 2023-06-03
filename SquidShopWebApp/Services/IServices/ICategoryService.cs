using SquidShopWebApp.Models;

namespace SquidShopWebApp.Services.IServices
{
	public interface ICategoryService
	{
		Task<T> GetAllAsync<T>();
		Task<T> CreateAsync<T>(Category dto);

    }
}
