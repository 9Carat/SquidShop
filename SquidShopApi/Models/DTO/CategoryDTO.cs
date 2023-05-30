using System.ComponentModel.DataAnnotations;

namespace SquidShopApi.Models.DTO
{
    public class CategoryDTO
    {
        [StringLength(50)]
        public string CategoryName { get; set; }
        [StringLength(200)]
        public string Details { get; set; }
    }
}
