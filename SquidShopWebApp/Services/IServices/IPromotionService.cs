﻿using SquidShopWebApp.Models;
using System.Collections;
using System.Linq.Expressions;

namespace SquidShopWebApp.Services.IServices
{
    public interface IPromotionService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetByIdAsync<T>(int id, Expression<Func<Promotion, bool>> filter = null, bool tracked = true);
        Task<T> CreateAsync<T>(Promotion entity);
        Task<T> DeleteAsync<T>(Promotion entity);
        Task<T> UpdateAsync<T>(Promotion entity);
      //  bool Any(Func<object, bool> value);
        //IQueryable<Promotion> GetPromotionsIncludingProducts();
        //IQueryable<Product> GetProductsIncludingProducts();

        Task<List<Product>> GetAllProductsAsync();
        Task<T> GetProductByIdAsync<T>(int id);
        
        Task SaveChangesAsync();
    }
}
