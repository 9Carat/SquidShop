﻿using System.Net;

namespace SquidShopWebApp.Models
{
	public class ApiResponse
	{
		public HttpStatusCode StatusCode { get; set; }
		public bool IsSuccess { get; set; } = true;
		public List <string> Errors { get; set; }
		public object Result { get; set; }
	}
}
