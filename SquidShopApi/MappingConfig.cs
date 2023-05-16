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
			CreateMap<Product, ProductUpdateDTO>().ReverseMap();
			CreateMap<Order, OrderDTO>().ReverseMap();
			CreateMap<OrderList, OrderListDTO>().ReverseMap();
			CreateMap<OrderList, OrderListUpdateDTO>().ReverseMap();
		}
    }
}
