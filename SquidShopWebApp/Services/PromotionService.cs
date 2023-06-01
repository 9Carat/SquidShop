﻿using Microsoft.EntityFrameworkCore;
using SquidShopWebApp.Data;
using SquidShopWebApp.Models;
using SquidShopWebApp.Services.IServices;
using SquidShopWebApp.Utility;
using System.Linq.Expressions;

namespace SquidShopWebApp.Services
{
    public class PromotionService : BaseService, IPromotionService
    {
        private readonly DbSet<Promotion> _promotionService;
        private readonly string _promotionUrl;
        public PromotionService(ApplicationDbContext dbContext, IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
        {
            _promotionService = dbContext.Set<Promotion>();
            this.httpClient = httpClient;
            this._promotionUrl = configuration.GetValue<string>("ServiceUrls:SquidShopApi");
        }

        public Task<T>CreateAsync<T>(Promotion entity)
        {
            return SendAsync<T>(apiRequest: new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = entity,
                ApiUrl = this._promotionUrl + "/promotion"
            });
        }
        //ADD
        public Task<T>GetAllAsync<T>()
        {
            return SendAsync<T>(apiRequest: new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                ApiUrl = this._promotionUrl + "/promotion"
            });
        }

        public Task<T>GetByIdAsync<T>(int id, Expression<Func<Promotion, bool>> filter = null, bool tracked = true)
        {
            return SendAsync<T>(apiRequest: new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                ApiUrl = this._promotionUrl + "/promotion/" +id
            });
        }

        public Task<T>DeleteAsync<T>(Promotion entity)
        {
            return SendAsync<T>(apiRequest: new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = entity,
                ApiUrl = this._promotionUrl + "/promotion" + entity.PromotionId
            });
        }

        public Task<T>UpdateAsync<T>(Promotion entity)
        {
            return SendAsync<T>(apiRequest: new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = entity,
                ApiUrl = this._promotionUrl + "/promotion" + entity.PromotionId
            });
        }

        bool IPromotionService.Any(Func<object, bool> value)
        {
            return GetPromotionsIncludingProducts().Any(value);
        }

        public IQueryable<Promotion> GetPromotionsIncludingProducts()
        {
            return _promotionService.Include(p => p.Product);
        }

        IQueryable<Product> IPromotionService.GetProductsIncludingProducts()
        {
            return _promotionService.Include(p => p.Product).Select(p => p.Product);
        }
    }
}