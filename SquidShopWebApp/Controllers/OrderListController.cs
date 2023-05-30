using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SquidShopWebApp.Data;
using SquidShopWebApp.Models;
using SquidShopWebApp.Models.DTO;
using SquidShopWebApp.Services;
using SquidShopWebApp.Services.IServices;

namespace SquidShopWebApp.Controllers
{
                                          // Test to see if Service worked
    public class OrderListController : Controller
    {
        private readonly IOrderListService _context;
        private readonly IMapper _mapper;

        public OrderListController(IOrderListService context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<OrderList> list = new();
            var response = await _context.GetAllAsync<ApiResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<OrderList>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FK_ProductId,Fk_OrderId,Price,Quantity")] OrderListCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _context.CreateAsync<ApiResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _context.GetByIdAsync<ApiResponse>(id);
            if (response != null && response.IsSuccess)
            {
                OrderList model = JsonConvert.DeserializeObject<OrderList>(Convert.ToString(response.Result));
                return View(_mapper.Map<OrderListUpdateDTO>(model));
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderListId,FK_ProductId,Fk_OrderId,Price,Quantity")] OrderListUpdateDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _context.UpdateAsync<ApiResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _context.GetByIdAsync<ApiResponse>(id);
            if (response != null && response.IsSuccess)
            {
                OrderList model = JsonConvert.DeserializeObject<OrderList>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(OrderList model)
        {
            var response = await _context.DeleteAsync<ApiResponse>(model.OrderListId);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }

}
