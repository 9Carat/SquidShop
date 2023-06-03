using SquidShopWebApp.Models;
using SquidShopWebApp.Models.DTO;

namespace SquidShopWebApp.Services.IServices
{
    public interface IPromotionService
    {
        Task<T>CreateAsync<T>(PromotionCreateDTO dto);
        Task<T> GetPromotionByIdAsync<T>(int id);
        Task<T> UpdatePromotionAsync<T>(Promotion promotion);
        Task<T> DeletePromotionAsync<T>(int id);
        Task<T> GetPromotionAsync<T>();
        Task<T> GetProductsAsync<T>();
        Task<T> UpdateProductAsync<T>(Product product);
      //  Task<T> GetAllProductsAsync<T>();
        Task SaveChangesAsync ();
        public Task<T> GetProductByIdAsync<T>(int id);

    }
}
