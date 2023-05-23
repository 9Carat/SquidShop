using Microsoft.AspNetCore.Mvc.ApiExplorer;
using SquidShopWebApp.Models;

namespace SquidShopWebApp.Services.IServices
{
	public interface IBaseService
	{
		ApiResponse response { get; set; }
		Task<T> SendAsync<T>(ApiRequest apiRequest);
	}
}
