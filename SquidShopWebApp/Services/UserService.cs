using SquidShopWebApp.Models;
using SquidShopWebApp.Models.DTO;
using SquidShopWebApp.Services.IServices;
using SquidShopWebApp.Utility;

namespace SquidShopWebApp.Services
{
    public class UserService : BaseService, IUserService
    {
        private string _userUrl;
        public UserService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
        {
            this.httpClient = httpClient;
            this._userUrl = configuration.GetValue<string>("ServiceUrls:SquidShopApi");
        }

        public Task<T> CreateAsync<T>(UserCreateDTO dto)
        {
            return SendAsync<T>(apiRequest: new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                ApiUrl = this._userUrl + "/user"
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(apiRequest: new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                ApiUrl = this._userUrl + "/user/" + id
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(apiRequest: new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                ApiUrl = this._userUrl + "/user"
            });
        }
        public Task<T> GetByIdAsync<T>(int id)
        {
            return SendAsync<T>(apiRequest: new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                ApiUrl = this._userUrl + "/user/" + id
            });
        }
        public Task<T> GetByFkIdAsync<T>(string id)
        {
            return SendAsync<T>(apiRequest: new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                ApiUrl = this._userUrl + "/user/" + id
            });
        }

        public Task<T> UpdateAsync<T>(UserUpdateDTO dto)
        {
            return SendAsync<T>(apiRequest: new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                ApiUrl = this._userUrl + "/user/" + dto.UserId
            });
        }
    }
}