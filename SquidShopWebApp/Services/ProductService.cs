using SquidShopWebApp.Models;
using SquidShopWebApp.Models.DTO;
using SquidShopWebApp.Services.IServices;

namespace SquidShopWebApp.Services
{
	public class ProductService : BaseService, IProductService
	{
		private string _productUrl;
		public ProductService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
		{
			this.httpClient = httpClient;
			this._productUrl = configuration.GetValue<string>("ServiceUrls:SquidShopApi");
		}

		public Task<T> CreateAsync<T>(ProductCreateDTO dto)
		{
			return SendAsync<T>(apiRequest: new ApiRequest()
			{
				ApiType = Utility.SD.ApiType.POST,
				Data = dto,
				ApiUrl = this._productUrl + "/product"
			});
		}

		public Task<T> DeleteAsync<T>(int id)
		{
			return SendAsync<T>(apiRequest: new ApiRequest()
			{
				ApiType= Utility.SD.ApiType.DELETE,
				ApiUrl = this._productUrl + "/product/" + id
			});
		}

		public Task<T> GetAllAsync<T>()
		{
			return SendAsync<T>(apiRequest: new ApiRequest()
			{
				ApiType = Utility.SD.ApiType.GET,
				ApiUrl = this._productUrl + "/product"
			});
		}

		public Task<T> GetByIdAsync<T>(int id)
		{
			return SendAsync<T>(apiRequest: new ApiRequest()
			{
				ApiType = Utility.SD.ApiType.GET,
				ApiUrl = this._productUrl + "/product/" + id
			});
		}

		public Task<T> UpdateAsync<T>(ProductUpdateDTO dto)
		{
			return SendAsync<T>(apiRequest: new ApiRequest()
			{
				ApiType = Utility.SD.ApiType.PUT,
				Data = dto,
				ApiUrl = this._productUrl + "/product/" + dto.ProductId
			});
		}
	}
}
