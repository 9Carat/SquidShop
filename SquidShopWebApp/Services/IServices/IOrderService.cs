using SquidShopWebApp.Models.DTO;

namespace SquidShopWebApp.Services.IServices
{
    public interface IOrderService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetOrderByIdAsync<T>(int id);
        Task<T> GetProductByIdAsync<T>(int id);
        Task<T> CreateOrderAsync<T>(OrderCreateDTO dto);
        Task<T> CreateOrderListAsync<T>(OrderListCreateDTO dto);
        Task<T> UpdateOrderAsync<T>(OrderUpdateDTO dto);
        Task<T> UpdateProductAsync<T>(ProductUpdateDTO dto);
        Task<T> DeleteAsync<T>(int id);
    }
}
