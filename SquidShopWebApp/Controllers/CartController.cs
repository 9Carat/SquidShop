using Microsoft.AspNetCore.Mvc;
using SquidShopWebApp.Models;
using AutoMapper;
using SquidShopWebApp.Services.IServices;
using Azure;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using SquidShopWebApp.Data;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using SquidShopWebApp.Models.DTO;
using SquidShopWebApp.Services;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SquidShopWebApp.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _user;
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _db;
        public CartController(IProductService productService, IOrderService orderService, IMapper mapper, UserManager<IdentityUser> user, IUserService userService, ApplicationDbContext db)
        {
            _productService = productService;
            _mapper = mapper;
            _user = user;
            _db = db;
            _userService = userService;
            _orderService = orderService;
        }
        public async Task<IActionResult> AddToCart(int productId)
        {
            int quantity = 1;

            var userId = await _user.GetUserAsync(User);

            Cart cart = GetCart(userId.Id);

            if (cart.CartItems.Count != 0)
            {
                CartItem existingCartItem = cart.CartItems.FirstOrDefault(item => item.Fk_ProductId == productId);
                if (existingCartItem != null)
                {
                    existingCartItem.Quantity += quantity;
                }
                else
                {
                    var response = await _productService.GetByIdAsync<ApiResponse>(productId);
                    Product product = JsonConvert.DeserializeObject<Product>(Convert.ToString(response.Result));
                    CartItem cartItem = new()
                    {
                        Fk_ProductId = productId,
                        Quantity = quantity,
                        UnitPrice = product.UnitPrice,
                        Discount = product.Discount,
                        DiscountUnitPrice = product.DiscountUnitPrice,
                        Fk_CartId = cart.CartId,
                        ProductName = product.ProductName,
                        ImageName = product.ImageName,
                    };
                    await _db.CartItems.AddAsync(cartItem);
                }
                await _db.SaveChangesAsync();
                return RedirectToAction("CartView");
            }
            else
            {
                var response = await _productService.GetByIdAsync<ApiResponse>(productId);
                Product product = JsonConvert.DeserializeObject<Product>(Convert.ToString(response.Result));
                CartItem cartItem = new()
                {
                    Fk_ProductId = productId,
                    Quantity = quantity,
                    UnitPrice = product.UnitPrice,
                    Discount = product.Discount,
                    DiscountUnitPrice = product.DiscountUnitPrice,
                    Fk_CartId = cart.CartId,
                    ProductName = product.ProductName,
                    ImageName = product.ImageName,
                };
                await _db.CartItems.AddAsync(cartItem);
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("CartView");
        }

        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            CartItem cartItem = await _db.CartItems.FindAsync(cartItemId);

            if (cartItem != null)
            {
                _db.CartItems.Remove(cartItem);
                _db.SaveChanges();
            }

            return RedirectToAction("CartView");
        }

        public async Task<IActionResult> UpdateQuantity(int cartItemId, int quantity)
        {
            CartItem cartItem = await _db.CartItems.FindAsync(cartItemId);

            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                _db.SaveChanges();
            }
            if (quantity == 0 || quantity <= 0)
            {
                _db.CartItems.Remove(cartItem);
                _db.SaveChanges();
            }

            return RedirectToAction("CartView");
        }

        public async Task<IActionResult> CartView()
        {

            var userId = await _user.GetUserAsync(User);

            Cart cart = GetCart(userId.Id);

            return View(cart);
        }

        private Cart GetCart(string userId)
        {
            Cart cart = _db.Carts.Include(i => i.CartItems).FirstOrDefault(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                };
                _db.Carts.Add(cart);
                _db.SaveChanges();
            }
            return cart;
        }
        public async Task<IActionResult> CreateUser()
        {
            var userId = await _user.GetUserAsync(User);
            var apiResponse = await _userService.GetByFkIdAsync<ApiResponse>(userId.Id);
            var user = JsonConvert.DeserializeObject<User>(Convert.ToString(apiResponse.Result));
            if (apiResponse.Result != null && apiResponse.IsSuccess)
            {

                if (userId.Id == user.FK_UsersId)
                {
                    return RedirectToAction(nameof(CreateOrder));
                }
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(UserCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                var userId = await _user.GetUserAsync(User);
                var apiResponse = await _userService.GetByFkIdAsync<ApiResponse>(userId.Id);
                var user = JsonConvert.DeserializeObject<User>(Convert.ToString(apiResponse.Result));
                
                model.FK_UsersId = userId.Id;
                var response = await _userService.CreateAsync<ApiResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(CreateOrder));
                }
            }
            return View(model);
        }
        public async Task<IActionResult> CreateOrder()
        {
            var userId = await _user.GetUserAsync(User);
            Cart cart = GetCart(userId.Id);
            return View(cart);
        }

        //Post Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrder(int id)
        {
            var userId = await _user.GetUserAsync(User);
            Cart cart = GetCart(userId.Id);
            //                                                Use when api is updated
            var apiResponse = await _userService.GetByFkIdAsync<ApiResponse>(userId.Id);
            var user = JsonConvert.DeserializeObject<User>(Convert.ToString(apiResponse.Result));
            //if (user == null)
            //{
            //    RedirectToAction(nameof(Create));
            //}


            var order = new OrderCreateDTO();
            order.FK_UserId = user.UserId;
            order.CreatedAt = DateTime.Now;
            order.OrderStatus = true;
            order.ShippingAddress = user.Address + ", " + user.City;
            var newOrder = await _orderService.CreateOrderAsync<ApiResponse>(order);
            var orderInfo = JsonConvert.DeserializeObject<Order>(Convert.ToString(newOrder.Result));
                
            foreach (var item in cart.CartItems)
            {

                var response = await _orderService.GetProductByIdAsync<ApiResponse>(item.Fk_ProductId);
                var product = JsonConvert.DeserializeObject<Product>(Convert.ToString(response.Result));
                   
                var orderList = new OrderListCreateDTO();
                    
                orderList.Fk_OrderId = orderInfo.OrderId;
                orderList.FK_ProductId = item.Fk_ProductId;
                orderList.Quantity = item.Quantity;
                if (product.Discount == 0)
                {
                    orderList.Price = item.UnitPrice * item.Quantity;
                }
                else
                {
                    orderList.Price = (double)(item.DiscountUnitPrice * item.Quantity);
                }
                product.InStock -= item.Quantity;
                var productUpdate = _mapper.Map<ProductUpdateDTO>(product);
                await _orderService.UpdateProductAsync<ApiResponse>(productUpdate);
                var newOrderList = _mapper.Map<OrderListCreateDTO>(orderList);
                await _orderService.CreateOrderListAsync<ApiResponse>(newOrderList);
                    
                _db.CartItems.Remove(item);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(CartView)); //View(cart);
        }
    }
}