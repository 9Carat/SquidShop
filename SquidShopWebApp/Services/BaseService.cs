﻿using Newtonsoft.Json;
using SquidShopWebApp.Models;
using SquidShopWebApp.Services.IServices;
using System.Text;

namespace SquidShopWebApp.Services
{
	public class BaseService : IBaseService
	{
        public ApiResponse response { get; set; }
        public IHttpClientFactory httpClient { get; set; }
        public BaseService(IHttpClientFactory httpClient)
        {
            this.response = new();
            this.httpClient = httpClient;
        }

        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {
                var client = httpClient.CreateClient("SquidShopApi");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.ApiUrl);

                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8, "application/json");
                }
                switch (apiRequest.ApiType)
                {
                    case Utility.SD.ApiType.POST:
						message.Method = HttpMethod.Post;
						break;
                    case Utility.SD.ApiType.PUT:
						message.Method = HttpMethod.Put;
						break;
                    case Utility.SD.ApiType.PATCH:
						message.Method = HttpMethod.Patch;
						break;
                    case Utility.SD.ApiType.DELETE:
						message.Method = HttpMethod.Delete;
						break;
                    default:
						message.Method = HttpMethod.Get;
						break;
                }
                HttpResponseMessage response = null;
                response = await client.SendAsync(message);
                var apiContent = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<T>(apiContent);
                return apiResponse;
            }
            catch (Exception ex)
            {
                var dto = new ApiResponse()
                {
                    Errors = new List<string> { Convert.ToString(ex.Message)},
                    IsSuccess = false
                };
                var result = JsonConvert.SerializeObject(dto);
                var apiResponse = JsonConvert.DeserializeObject<T>(result);
                return apiResponse;
            }
        }
	}
}
