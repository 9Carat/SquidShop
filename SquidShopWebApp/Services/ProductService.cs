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

		public Task<T> CreateAsync<T>(T entity)
		{
			throw new NotImplementedException();
		}

		public Task<T> DeleteAsync<T>(int id)
		{
			throw new NotImplementedException();
		}

		public Task<T> GetAllAsync<T>()
		{
			return SendAsync<T>(apiRequest: new Models.ApiRequest()
			{
				ApiType = Utility.SD.ApiType.GET,
				ApiUrl = this._productUrl + "/product"
			});
		}

		public Task<T> GetByIdAsync<T>(int id)
		{
			throw new NotImplementedException();
		}

		public Task<T> UpdateAsync<T>(T entity)
		{
			throw new NotImplementedException();
		}
	}
}
