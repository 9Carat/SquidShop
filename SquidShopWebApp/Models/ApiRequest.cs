using Microsoft.AspNetCore.Mvc;
using static SquidShopWebApp.Utility.SD;

namespace SquidShopWebApp.Models
{
	public class ApiRequest
	{
		public ApiType ApiType { get; set; } = ApiType.GET;
		public string ApiUrl { get; set; }
		public object Data { get; set; }
	}
}
