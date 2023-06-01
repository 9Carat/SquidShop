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
			CreateMap<Product, ProductUpdateDTO>().ReverseMap();
            CreateMap<Order, OrderCreateDTO>().ReverseMap();
            CreateMap<Order, OrderUpdateDTO>().ReverseMap();
            CreateMap<OrderList, OrderListCreateDTO>().ReverseMap();
            CreateMap<Promotion, Promotion>().ReverseMap();
        }
    }
}
