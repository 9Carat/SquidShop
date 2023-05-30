using SquidShopWebApp.Models.DTO;

namespace SquidShopWebApp.Services.IServices
{
	public interface IOrderListService
	{
		Task<T> GetAllAsync<T>();
		Task<T> GetByIdAsync<T>(int id);
		Task<T> CreateAsync<T>(OrderListCreateDTO dto);
		Task<T> UpdateAsync<T>(OrderListUpdateDTO dto);
		Task<T> DeleteAsync<T>(int id);
	}
}
