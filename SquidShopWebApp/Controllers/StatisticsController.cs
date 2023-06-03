using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SquidShopWebApp.Models;
using SquidShopWebApp.Services.IServices;

namespace SquidShopWebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StatisticsController : Controller
    {
        private readonly IOrderService _orderService;
        public StatisticsController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<IActionResult> Index()
        {
            List<Order> orderList = new();
            var orderResponse = await _orderService.GetAllOrdersAsync<ApiResponse>();
            if (orderResponse != null && orderResponse.IsSuccess)
            {
                orderList = JsonConvert.DeserializeObject<List<Order>>(Convert.ToString(orderResponse.Result));
            }
            List<OrderList> orderListList = new();
            var orderListResponse = await _orderService.GetAllOrderListsAsync<ApiResponse>();
            if (orderListResponse != null && orderListResponse.IsSuccess)
            {
                orderListList = JsonConvert.DeserializeObject<List<OrderList>>(Convert.ToString(orderListResponse.Result));
            }
            List<Product> productList = new();
            var ProductListResponse = await _orderService.GetAllProductsAsync<ApiResponse>();
            if (ProductListResponse != null && ProductListResponse.IsSuccess)
            {
                productList = JsonConvert.DeserializeObject<List<Product>>(Convert.ToString(ProductListResponse.Result));
            }

            List<StatisticsViewModel> statisticsList = new();

            var items = (from o in orderList
                         join ol in orderListList on o.OrderId equals ol.Fk_OrderId
                         join p in productList on ol.FK_ProductId equals p.ProductId
                         where o.OrderId == ol.Fk_OrderId
                         select new
                         {
                             product = p,
                             order = o,
                             orderList = ol,
                         }).ToList();

            foreach (var item in items)
            {
                StatisticsViewModel listItem = new();
                listItem.Product = item.product;
                listItem.Order = item.order;
                listItem.OrderList = item.orderList;
                statisticsList.Add(listItem);
            }

            return View(statisticsList);
        }

        public IActionResult Details()
        {
            return View();
        }

        public async Task<IActionResult> DetailsTimeSpan(DateTime startDate, DateTime endDate)
        {
            List<Order> orderList = new();
            var orderResponse = await _orderService.GetAllOrdersAsync<ApiResponse>();
            if (orderResponse != null && orderResponse.IsSuccess)
            {
                orderList = JsonConvert.DeserializeObject<List<Order>>(Convert.ToString(orderResponse.Result));
            }
            List<OrderList> orderListList = new();
            var orderListResponse = await _orderService.GetAllOrderListsAsync<ApiResponse>();
            if (orderListResponse != null && orderListResponse.IsSuccess)
            {
                orderListList = JsonConvert.DeserializeObject<List<OrderList>>(Convert.ToString(orderListResponse.Result));
            }
            List<Product> productList = new();
            var ProductListResponse = await _orderService.GetAllProductsAsync<ApiResponse>();
            if (ProductListResponse != null && ProductListResponse.IsSuccess)
            {
                productList = JsonConvert.DeserializeObject<List<Product>>(Convert.ToString(ProductListResponse.Result));
            }

            List<StatisticsViewModel> statisticsList = new();

            var items = (from o in orderList
                         join ol in orderListList on o.OrderId equals ol.Fk_OrderId
                         join p in productList on ol.FK_ProductId equals p.ProductId
                         where o.OrderId == ol.Fk_OrderId && o.CreatedAt > startDate && o.CreatedAt < endDate
                         select new
                         {
                             product = p,
                             order = o,
                             orderList = ol,
                         }).ToList();

            foreach (var item in items)
            {
                StatisticsViewModel listItem = new();
                listItem.Product = item.product;
                listItem.Order = item.order;
                listItem.OrderList = item.orderList;
                statisticsList.Add(listItem);
            }

            return View(statisticsList);
        }

        public IActionResult ProductDetails(int id)
        {
            StatisticsDateViewModel model = new();
            model.ProductId = id;
            return View(model);
        }

        public async Task<IActionResult> ProductDetailsResult(StatisticsDateViewModel model)
        {
            List<Order> orderList = new();
            var orderResponse = await _orderService.GetAllOrdersAsync<ApiResponse>();
            if (orderResponse != null && orderResponse.IsSuccess)
            {
                orderList = JsonConvert.DeserializeObject<List<Order>>(Convert.ToString(orderResponse.Result));
            }
            List<OrderList> orderListList = new();
            var orderListResponse = await _orderService.GetAllOrderListsAsync<ApiResponse>();
            if (orderListResponse != null && orderListResponse.IsSuccess)
            {
                orderListList = JsonConvert.DeserializeObject<List<OrderList>>(Convert.ToString(orderListResponse.Result));
            }
            List<Product> productList = new();
            var ProductListResponse = await _orderService.GetAllProductsAsync<ApiResponse>();
            if (ProductListResponse != null && ProductListResponse.IsSuccess)
            {
                productList = JsonConvert.DeserializeObject<List<Product>>(Convert.ToString(ProductListResponse.Result));
            }

            List<StatisticsViewModel> statisticsList = new();

            var items = (from o in orderList
                         join ol in orderListList on o.OrderId equals ol.Fk_OrderId
                         join p in productList on ol.FK_ProductId equals p.ProductId
                         where o.OrderId == ol.Fk_OrderId && o.CreatedAt > model.StartDate && o.CreatedAt < model.EndDate && p.ProductId == model.ProductId
                         select new
                         {
                             product = p,
                             order = o,
                             orderList = ol,
                         }).ToList();

            foreach (var item in items)
            {
                StatisticsViewModel listItem = new();
                listItem.Product = item.product;
                listItem.Order = item.order;
                listItem.OrderList = item.orderList;
                statisticsList.Add(listItem);
            }

            return View(statisticsList);
        }
    }
}
