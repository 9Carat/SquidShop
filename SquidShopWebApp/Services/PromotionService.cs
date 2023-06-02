using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SquidShopWebApp.Data;
using SquidShopWebApp.Models;
using SquidShopWebApp.Models.DTO;
using SquidShopWebApp.Services.IServices;
using SquidShopWebApp.Utility;
using System.Collections;
using System.Configuration;
using System.Linq.Expressions;


namespace SquidShopWebApp.Services
{
    public class PromotionService : BaseService, IPromotionService
    {

        private readonly string _promotionUrl;
        private readonly string _productUrl;
        public PromotionService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
        {

            this.httpClient = httpClient;
            this._promotionUrl = configuration.GetValue<string>("ServiceUrls:SquidShopApi");
            this._productUrl = configuration.GetValue<string>("ServiceUrls:SquidShopApi");
        }

        public Task<T> CreateAsync<T>(Promotion entity)
        {
            return SendAsync<T>(apiRequest: new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = entity,
                ApiUrl = this._promotionUrl + "/promotion"

            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(apiRequest: new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                ApiUrl = this._promotionUrl + "/promotion"
            });
        }

        public Task<T> GetByIdAsync<T>(int id, Expression<Func<Promotion, bool>> filter = null, bool tracked = true)
        {
            return SendAsync<T>(apiRequest: new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                ApiUrl = this._promotionUrl + "/promotion/" + id
            });
        }

        public Task<T> DeleteAsync<T>(Promotion entity)
        {
            return SendAsync<T>(apiRequest: new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = entity,
                ApiUrl = this._promotionUrl + "/promotion" + entity.PromotionId
            });
        }

        public Task<T> UpdateAsync<T>(Promotion entity)
        {
            return SendAsync<T>(apiRequest: new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = entity,
                ApiUrl = this._promotionUrl + "/promotion" + entity.PromotionId
            });
        }

        //bool IPromotionService.Any(Func<object, bool> value)
        //{
        //    return GetPromotionsIncludingProducts().Any(value);
        //}



        public async Task<T> GetAllProductAsync<T>()
        {
            return await SendAsync<T>(apiRequest: new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                ApiUrl = this._productUrl + "/product"
            });
        }


        public async Task<List<Product>> GetAllProductsAsync()
        {
            var response = await SendAsync<ApiResponse>(apiRequest: new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                ApiUrl = this._productUrl + "/product"
            });

            if (response != null && response.IsSuccess)
            {
                var products = JsonConvert.DeserializeObject<List<Product>>(Convert.ToString(response.Result));
                return products;
            }

            return new List<Product>();
        }
        public Task<T> GetProductByIdAsync<T>(int id)
        {
            return SendAsync<T>(apiRequest: new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                ApiUrl = this._productUrl + "/product/" + id
            });
        }

        Task<T> IPromotionService.UpdateProductAsync<T>(Product entity)
        {
            return SendAsync<T>(apiRequest: new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = entity,
                ApiUrl = this._productUrl + "/product/" + entity.ProductId
            });
        }
    }
}

    
