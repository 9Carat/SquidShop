using AutoMapper;
using SquidShopApi.Models;
using SquidShopApi.Models.DTO;

namespace SquidShopApi
{
	public class MappingConfig : Profile
	{
        public MappingConfig()
        {
			CreateMap<Product, ProductDTO>().ReverseMap();
		}
    }
}
