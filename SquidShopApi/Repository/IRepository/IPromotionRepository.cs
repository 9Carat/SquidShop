using SquidShopApi.Models;
using System.Linq.Expressions;

namespace SquidShopApi.Repository.IRepository
{
    public interface IPromotionRepository
    {
        Task<List<Promotion>> GetAllAsync();
        Task<Promotion> GetByIdAsync(Expression<Func<Promotion, bool>> filter = null, bool tracked = true);
        Task CreateAsync(Promotion entity);
        Task RemoveAsync(Promotion entity);
        Task UpdateAsync(Promotion entity);
        Task SaveAsync();
    }
}