using SquidShopWebApp.Models.DTO;
using SquidShopWebApp.Models;
using SquidShopWebApp.Utility;
using SquidShopWebApp.Services.IServices;

namespace SquidShopWebApp.Services
{
	public class OrderListService : BaseService, IOrderListService
	{
		private readonly string _orderListUrl;
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
				ApiUrl = this._orderListUrl + "/orderlist"
			});
		}

		public Task<T> DeleteAsync<T>(int id)
		{
			return SendAsync<T>(apiRequest: new ApiRequest()
			{
				ApiType = SD.ApiType.DELETE,
				ApiUrl = this._orderListUrl + "/orderlist/" + id
			});
		}

		public Task<T> GetAllAsync<T>()
		{
			return SendAsync<T>(apiRequest: new ApiRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = this._orderListUrl + "/orderlist"
			});
		}
		public Task<T> GetByIdAsync<T>(int id)
		{
			return SendAsync<T>(apiRequest: new ApiRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = this._orderListUrl + "/orderlist/" + id
			});
		}

		public Task<T> UpdateAsync<T>(OrderListUpdateDTO dto)
		{
			return SendAsync<T>(apiRequest: new ApiRequest()
			{
				ApiType = SD.ApiType.PUT,
				Data = dto,
				ApiUrl = this._orderListUrl + "/orderlist/" + dto.OrderListId
			});
		}
	}
}
