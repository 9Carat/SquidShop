using SquidShopWebApp.Models.DTO;

namespace SquidShopWebApp.Services.IServices
{
    public interface IOrderService
    {
        Task<T> GetAllOrdersAsync<T>();
        Task<T> GetAllOrderListsAsync<T>();
        Task<T> GetAllProductsAsync<T>();
        Task<T> GetOrderByIdAsync<T>(int id);
        Task<T> GetOrderListByIdAsync<T>(int id);
        Task<T> GetProductByIdAsync<T>(int id);
        Task<T> CreateOrderAsync<T>(OrderCreateDTO dto);
        Task<T> CreateOrderListAsync<T>(OrderListCreateDTO dto);
        Task<T> UpdateOrderAsync<T>(OrderUpdateDTO dto);
        Task<T> UpdateProductAsync<T>(ProductUpdateDTO dto);
        Task<T> DeleteAsync<T>(int id);
    }
}
