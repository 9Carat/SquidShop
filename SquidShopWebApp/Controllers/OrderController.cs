using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SquidShopWebApp.Models.DTO;
using SquidShopWebApp.Models;
using SquidShopWebApp.Services.IServices;

namespace SquidShopWebApp.Controllers
{
                                    // Test to see if Service worked
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
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
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _orderService.CreateAsync<ApiResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int interestid)
        {
            var response = await _orderService.GetByIdAsync<ApiResponse>(interestid);
            if (response != null && response.IsSuccess)
            {
                Order model = JsonConvert.DeserializeObject<Order>(Convert.ToString(response.Result));
                return View(_mapper.Map<OrderUpdateDTO>(model));
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(OrderUpdateDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _orderService.UpdateAsync<ApiResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int interestid)
        {
            var response = await _orderService.GetByIdAsync<ApiResponse>(interestid);
            if (response != null && response.IsSuccess)
            {
                Order model = JsonConvert.DeserializeObject<Order>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Order model)
        {
            if (ModelState.IsValid)
            {
                var response = await _orderService.DeleteAsync<ApiResponse>(model.OrderId);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }
    }
}
