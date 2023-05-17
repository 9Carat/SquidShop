using System.ComponentModel.DataAnnotations;

namespace SquidShopApi.Models.DTO
{
    public class CategoryUpdateDTO
    {
        public int CategoryId { get; set; }
        [StringLength(50)]
        public string CategoryName { get; set; }
        [StringLength(200)]
        public string Details { get; set; }
    }
}
