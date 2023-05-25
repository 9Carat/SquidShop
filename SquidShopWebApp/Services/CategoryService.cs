﻿using SquidShopWebApp.Models;
using SquidShopWebApp.Services.IServices;

namespace SquidShopWebApp.Services
{
	public class CategoryService : BaseService, ICategoryService
	{
		private string _categoryUrl;
		public CategoryService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
		{
			this.httpClient = httpClient;
			this._categoryUrl = configuration.GetValue<string>("ServiceUrls:SquidShopApi");
		}

		public Task<T> GetAllAsync<T>()
		{
			return SendAsync<T>(apiRequest: new ApiRequest()
			{
				ApiType = Utility.SD.ApiType.GET,
				ApiUrl = this._categoryUrl + "/category"
			});
		}
	}
}
