using SquidShopWebApp.Models.DTO;

namespace SquidShopWebApp.Services.IServices
{
    public interface IUserService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetByIdAsync<T>(int id);
        Task<T> CreateAsync<T>(UserCreateDTO dto);
        Task<T> UpdateAsync<T>(UserUpdateDTO dto);
        Task<T> DeleteAsync<T>(int id);
    }
}