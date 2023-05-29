using Microsoft.EntityFrameworkCore;
using SquidShopApi.Data;
using SquidShopApi.Models;
using SquidShopApi.Repository.IRepository;
using System.Linq.Expressions;

namespace SquidShopApi.Repository
{
    public class PromotionRepository : IPromotionRepository
    {
        private readonly ApplicationDbContext _context;
        public PromotionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Promotion entity)
        {
            await _context.Promotions.AddAsync(entity);
            await SaveAsync();
        }

        public async Task<List<Promotion>> GetAllAsync()
        {
            var result = _context.Promotions;
            return await result.ToListAsync();
        }

        public async Task<Promotion> GetByIdAsync(Expression<Func<Promotion, bool>> filter = null, bool tracked = true)
        {
            IQueryable<Promotion> query = _context.Promotions;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(Promotion entity)
        {
            _context.Promotions.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Promotion entity)
        {
            _context.Promotions.Update(entity);
            await SaveAsync();

        }
    }
}
