namespace SquidShopWebApp.Services.IServices
{
	public interface ICategoryService
	{
		Task<T> GetAllAsync<T>();
	}
}
