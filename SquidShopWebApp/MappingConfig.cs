using AutoMapper;
using SquidShopWebApp.Models;
using SquidShopWebApp.Models.DTO;

namespace SquidShopWebApp
{
	public class MappingConfig : Profile
	{
        public MappingConfig()
        {
			CreateMap<Product, ProductCreateDTO>().ReverseMap();

		}
    }
}
