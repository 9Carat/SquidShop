using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SquidShopWebApp.Models;
using SquidShopWebApp.Services.IServices;
using Newtonsoft.Json;
using SquidShopWebApp.Models.DTO;
using SquidShopWebApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace SquidShopWebApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        //Get Index
        public async Task<IActionResult> Index()
        {
            List<Order> list = new();
            var response = await _orderService.GetAllAsync<ApiResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<Order>>(Convert.ToString(response.Result));
            }
            return View(list);
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
                var order = new Order();
                var orderList = new OrderList();
                order.FK_UserId = viewModel.UserId; 
                order.CreatedAt = DateTime.Now;
                order.OrderStatus = true;
                order.ShippingAddress = viewModel.ShippingAddress;
                var newOrder = _mapper.Map<OrderCreateDTO>(order);
                await _orderService.CreateOrderAsync<ApiResponse>(newOrder);
                //return RedirectToAction("CreateOrderList", "OrderLists", viewModel);
                orderList.Fk_OrderId = order.OrderId;
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
    }
}
