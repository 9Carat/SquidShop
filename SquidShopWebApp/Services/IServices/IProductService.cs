using SquidShopWebApp.Models.DTO;

namespace SquidShopWebApp.Services.IServices
{
	public interface IProductService
	{
		Task<T> GetAllAsync<T>();
		Task<T> GetByIdAsync<T>(int id);
		Task<T> CreateAsync<T>(ProductCreateDTO dto);
		Task<T> UpdateAsync<T>(ProductUpdateDTO dto);
		Task<T> DeleteAsync<T>(int id);
	}
}
