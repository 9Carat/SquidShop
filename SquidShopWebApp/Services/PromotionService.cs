using SquidShopWebApp.Models;
using SquidShopWebApp.Models.DTO;
using SquidShopWebApp.Services.IServices;
using SquidShopWebApp.Utility;
using System.Configuration;
using Newtonsoft.Json;

namespace SquidShopWebApp.Services
{
    public class PromotionService : BaseService, IPromotionService
    {
        private readonly IConfiguration _configuration;
        private string _promotionUrl;
        private readonly string _productUrl;

        public PromotionService(IHttpClientFactory httpClient, IConfiguration configuration)
            : base(httpClient)
        {
            _configuration = configuration;
            this.httpClient = httpClient;
            this._promotionUrl = configuration.GetValue<string>("ServiceUrls:SquidShopApi");
            this._productUrl = configuration.GetValue<string>("ServiceUrls:SquidShopApi");
        }

        public Task<T> CreateAsync<T>(PromotionCreateDTO dto)
        {
            return SendAsync<T>(apiRequest: new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                ApiUrl = this._promotionUrl + "/promotion"
            });
        }

        public async Task<ApiResponse> UpdateProductAsync(Product product)
        {
            return await SendAsync<ApiResponse>(apiRequest: new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = product,
                ApiUrl = this._productUrl + "/product/" + product.ProductId
            });
        }
    

      
        public Task<T> GetPromotionByIdAsync<T>(int id)
        {
            return SendAsync<T>(apiRequest: new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                ApiUrl = $"{this._promotionUrl}/promotion"
            });
        }

        public Task<T> UpdatePromotionAsync<T>(Promotion promotion)
        {
            return SendAsync<T>(apiRequest: new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = promotion,
                ApiUrl = this._promotionUrl + "/promotion/" + promotion.PromotionId
            });
        }

        public Task<T> DeletePromotionAsync<T>(int id)
        {
            return SendAsync<T>(apiRequest: new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                ApiUrl = this._promotionUrl + "/promotion/" + id
            });
        }

        public Task<T> GetPromotionAsync<T>()
        {
            return SendAsync<T>(apiRequest: new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                ApiUrl = this._promotionUrl + "/promotion/"
            });
        }

        public Task<T> GetProductsAsync<T>()
        {
            return SendAsync<T>(apiRequest: new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                ApiUrl = this._productUrl + "/product/"
            });
        }

        public Task<T> UpdateProductAsync<T>(Product product)
        {
            return SendAsync<T>(apiRequest: new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                ApiUrl = this._productUrl + "/product"
            });
        }

        public Task<List<Product>> GetAllProductsAsync<T>()
        {
            return SendAsync<List<Product>>(apiRequest: new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                ApiUrl = this._productUrl + "/product"
            });
        }

        public async Task SaveChangesAsync()
        {
            await Task.CompletedTask;
        }

        public Task<T> GetProductByIdAsync<T>(int id)
        {
            return SendAsync<T>(apiRequest: new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                ApiUrl = $"{this._productUrl}/product/{id}"
            });
        }
    }
}
