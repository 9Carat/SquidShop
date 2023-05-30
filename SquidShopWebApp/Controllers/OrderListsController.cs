using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SquidShopWebApp.API;
using SquidShopWebApp.Data;
using SquidShopWebApp.Models;
using SquidShopWebApp.Models.DTO;
using SquidShopWebApp.Services.IServices;

namespace SquidShopWebApp.Controllers
{
    public class OrderListsController : Controller
    {
        private readonly IOrderListService _orderListService;
        private readonly IMapper _mapper;
        public OrderListsController(IOrderListService orderListService, IMapper mapper)
        {
            _orderListService = orderListService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        //public async Task<IActionResult> CreateOrderList(ProductViewModel viewModel)
        //{
        //    var orderList = new OrderList();
        //    orderList.Fk_OrderId = viewModel.OrderId;
        //    orderList.FK_ProductId = viewModel.ProductId;
        //    orderList.Quantity = (int)viewModel.Quantity;
        //    var newOrderList = _mapper.Map<OrderListCreateDTO>(orderList);
        //    await _orderListService.CreateAsync<ApiResponse>(newOrderList);
        //    return RedirectToAction("Index", "Orders");
        //}
    }
}
