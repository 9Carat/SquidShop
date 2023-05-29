using SquidShopWebApp.Models.DTO;
using SquidShopWebApp.Models;
using SquidShopWebApp.Utility;
using SquidShopWebApp.Services.IServices;

namespace SquidShopWebApp.Services
{
    public class OrderService : BaseService, IOrderService
    {
        private readonly string _orderUrl;
        public OrderService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
        {
            this.httpClient = httpClient;
            this._orderUrl = configuration.GetValue<string>("ServiceUrls:SquidShopApi");
        }

        public Task<T> CreateAsync<T>(OrderCreateDTO dto)
        {
            return SendAsync<T>(apiRequest: new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                ApiUrl = this._orderUrl + "/order"
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(apiRequest: new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                ApiUrl = this._orderUrl + "/order/" + id
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(apiRequest: new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                ApiUrl = this._orderUrl + "/order"
            });
        }
        public Task<T> GetByIdAsync<T>(int id)
        {
            return SendAsync<T>(apiRequest: new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                ApiUrl = this._orderUrl + "/order/" + id
            });
        }

        public Task<T> UpdateAsync<T>(OrderUpdateDTO dto)
        {
            return SendAsync<T>(apiRequest: new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                ApiUrl = this._orderUrl + "/order/" + dto.OrderId
            });
        }
    }
}
