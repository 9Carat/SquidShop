using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _user;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public OrdersController(IOrderService orderService, IMapper mapper, ApplicationDbContext db, IUserService userService, UserManager<IdentityUser> user)
        {
            _orderService = orderService;
            _mapper = mapper;
            _db = db;
            _userService = userService;
            _user = user;
        }
        //Get Index
        public async Task<IActionResult> Index()
        {
            List<Order> list = new();
            List<Order> selectList = new();
            var response = await _orderService.GetAllOrdersAsync<ApiResponse>();
            var userId = await _user.GetUserAsync(User);
            if (userId != null && response.IsSuccess)
            {
                var apiResponse = await _userService.GetByFkIdAsync<ApiResponse>(userId.Id);
                var user = JsonConvert.DeserializeObject<User>(Convert.ToString(apiResponse.Result));

                if (user != null && apiResponse.IsSuccess)
                {
                    list = JsonConvert.DeserializeObject<List<Order>>(Convert.ToString(response.Result));
                    selectList = list.Where(u=>u.FK_UserId == user.UserId).ToList();
                }
            }
            return View(selectList);
        }

        //Get Create
        public IActionResult Create()
        {
            return View();
        }

        //Post Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrder(ProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Hämtar user och product och skapar en ny order samt orderList
                // Bör förmodligen läggas upp på ett smidigare sätt :)

                //var user = await _userManager.GetUserAsync(User);
                var response = await _orderService.GetProductByIdAsync<ApiResponse>(viewModel.ProductId);
                var product = JsonConvert.DeserializeObject<Product>(Convert.ToString(response.Result));
                var order = new OrderCreateDTO();
                var orderList = new OrderListCreateDTO();
                order.FK_UserId = viewModel.UserId;
                order.CreatedAt = DateTime.Now;
                order.OrderStatus = true;
                order.ShippingAddress = viewModel.ShippingAddress;
                var newOrder = await _orderService.CreateOrderAsync<ApiResponse>(order);
                var orderInfo = JsonConvert.DeserializeObject<Order>(Convert.ToString(newOrder.Result));
                orderList.Fk_OrderId = orderInfo.OrderId;
                orderList.FK_ProductId = product.ProductId;
                orderList.Quantity = (int)viewModel.Quantity;
                if (product.Discount == 0)
                {
                    orderList.Price = product.UnitPrice * (int)viewModel.Quantity;
                }
                else
                {
                    orderList.Price = (double)(product.DiscountUnitPrice * (int)viewModel.Quantity);
                }
                product.InStock -= (int)viewModel.Quantity;
                var productUpdate = _mapper.Map<ProductUpdateDTO>(product);
                await _orderService.UpdateProductAsync<ApiResponse>(productUpdate);
                var newOrderList = _mapper.Map<OrderListCreateDTO>(orderList);
                await _orderService.CreateOrderListAsync<ApiResponse>(newOrderList);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            // Hämtar order
            var orderResponse = await _orderService.GetOrderByIdAsync<ApiResponse>((int)id);
            var orderInfo = JsonConvert.DeserializeObject<Order>(Convert.ToString(orderResponse.Result));

            if (orderInfo == null || id == null)
            {
                return NotFound();
            }

            // Hämtar orderList och product
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

            // Matchar orderList och product med order
            var orderlist = orderListList.Where(o => o.Fk_OrderId == id).ToList();
            //List<int> productId = new();
            //foreach(var order in orderlist)
            //{
            //    productId.Add(order.FK_ProductId);
            //}
            //var productId = orderlist.FK_ProductId;

            var products = (from ol in orderlist
                            join p in productList on ol.FK_ProductId equals p.ProductId
                            select p).ToList();

            //var product = productList.Where(p => p.ProductId == ).FirstOrDefault();


            //Anropar API för uträkning av distans
            var ApiResponse = new DistanceApi();
            var result = await ApiResponse.Get(orderInfo.ShippingAddress);

            var viewModel = new OrderViewModel() { Order = orderInfo, Product = products, OrderList = orderlist, ApiResponse = result };

            return View(viewModel);
        }
    }
}
