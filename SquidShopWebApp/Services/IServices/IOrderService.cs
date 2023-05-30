using SquidShopWebApp.Models.DTO;

namespace SquidShopWebApp.Services.IServices
{
	public interface IOrderService
	{
		Task<T> GetAllAsync<T>();
		Task<T> GetByIdAsync<T>(int id);
		Task<T> CreateAsync<T>(OrderCreateDTO dto);
		Task<T> UpdateAsync<T>(OrderUpdateDTO dto);
		Task<T> DeleteAsync<T>(int id);
	}
}
