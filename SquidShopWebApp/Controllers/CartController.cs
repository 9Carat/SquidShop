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

namespace SquidShopWebApp.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _user;
        private readonly ApplicationDbContext _db;
        public CartController(IProductService productService, IMapper mapper, UserManager<IdentityUser> user, ApplicationDbContext db)
        {
            _productService = productService;
            _mapper = mapper;
            _user = user;
            _db = db;
        }
        public async Task<IActionResult> AddToCart(int productId)
        {
            int quantity = 1;

            var userId = await _user.GetUserAsync(User);
            
            Cart cart = GetCart(userId.Id);

            
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
            if (cartItem.Quantity == 0) 
            {
                _db.CartItems.Remove(cartItem);
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
            Cart cart = _db.Carts.Include(i=>i.CartItems).FirstOrDefault(c => c.UserId == userId);

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
    }
}
