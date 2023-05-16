using System.Linq.Expressions;

namespace SquidShopApi.Repository.IRepository
{
	public interface IRepository<T> where T : class
	{
		Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);
		Task<T> GetByIdAsync(Expression<Func<T, bool>> filter = null, bool tracked = true);
		Task CreateAsync(T entity);
		Task RemoveAsync(T entity);
		Task UpdateAsync(T entity);
		Task<T> UpdatePartialAsync(T entity);
		Task SaveAsync();
	}
}
