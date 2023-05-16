using Microsoft.EntityFrameworkCore;
using SquidShopApi.Data;
using SquidShopApi.Repository.IRepository;
using System.Linq.Expressions;

namespace SquidShopApi.Repository
{
	public class GenericRepository<T> : IRepository<T> where T : class
	{
		private readonly ApplicationDbContext _db;
		internal DbSet<T> dbSet;
        public GenericRepository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();   
        }

		public async Task CreateAsync(T entity)
		{
			await _db.AddAsync(entity);
			await SaveAsync();
		}

		public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null)
		{
			IQueryable<T> temp = dbSet;
			if (filter != null) 
			{
				temp = temp.Where(filter);
			}
			return await temp.ToListAsync();
		}

		public async Task<T> GetByIdAsync(Expression<Func<T, bool>> filter = null, bool tracked = true)
		{
			IQueryable<T> temp = dbSet;
			if (tracked == true) 
			{
				temp = temp.AsNoTracking();
			}
			if (filter != null)
			{
				temp = temp.Where(filter);
			}
			return await temp.FirstOrDefaultAsync();
		}

		public async Task RemoveAsync(T entity)
		{
			_db.Remove(entity);
			await SaveAsync();
		}

		public async Task SaveAsync()
		{
			await _db.SaveChangesAsync();
		}

		public async Task UpdateAsync(T entity)
		{
			_db.Update(entity);
			await SaveAsync();
		}

		public async Task<T> UpdatePartialAsync(T entity)
		{
			_db.Update(entity);
			await SaveAsync();
			return entity;
		}
	}
}
