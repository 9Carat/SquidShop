using SquidShopWebApp.Models;
using SquidShopWebApp.Models.DTO;
using SquidShopWebApp.Services.IServices;
using SquidShopWebApp.Utility;

namespace SquidShopWebApp.Services
{
	public class OrderListService : BaseService, IOrderListService
	{
		private string _orderListUrl;
		public OrderListService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
		{
			this.httpClient = httpClient;
			this._orderListUrl = configuration.GetValue<string>("ServiceUrls:SquidShopApi");
		}

		public Task<T> CreateAsync<T>(OrderListCreateDTO dto)
		{
			return SendAsync<T>(apiRequest: new ApiRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = dto,
				ApiUrl = this._orderListUrl + "/orderList"
			});
		}

		public Task<T> DeleteAsync<T>(int id)
		{
			return SendAsync<T>(apiRequest: new ApiRequest()
			{
				ApiType= SD.ApiType.DELETE,
				ApiUrl = this._orderListUrl + "/orderList/" + id
			});
		}

		public Task<T> GetAllAsync<T>()
		{
			return SendAsync<T>(apiRequest: new ApiRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = this._orderListUrl + "/orderList"
			});
		}
		public Task<T> GetByIdAsync<T>(int id)
		{
			return SendAsync<T>(apiRequest: new ApiRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = this._orderListUrl + "/orderList/" + id
			});
		}

		public Task<T> UpdateAsync<T>(OrderListUpdateDTO dto)
		{
			return SendAsync<T>(apiRequest: new ApiRequest()
			{
				ApiType = SD.ApiType.PUT,
				Data = dto,
				ApiUrl = this._orderListUrl + "/orderList/" + dto.OrderListId
			});
		}
	}
}
