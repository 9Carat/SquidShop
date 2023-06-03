using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class PromotionsController : Controller
    {
        private readonly IPromotionService _promotionService;

        public PromotionsController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        // GET: Promotions
        public async Task<IActionResult> Index()
        {
            var promotions = await _promotionService.GetPromotionAsync<List<Promotion>>();
            return View(promotions);
        }

        // GET: Promotions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promotion = await _promotionService.GetPromotionByIdAsync<Promotion>(id.Value);
            if (promotion == null)
            {
                return NotFound();
            }

            return View(promotion);
        }

        // GET: Promotions/Create
        public async Task<IActionResult> CreateAsync()
        {
            List<Product>   ProductList = new();
            var productList = await _promotionService.GetProductsAsync<List<Product>>();
            if (productList != null && productList.Count > 0)
            {
                productList = productList.Select(p => new Product { ProductId = p.ProductId, ProductName = p.ProductName }).ToList();
            }
            return View(productList);
        }
           
        

        private double CalculateDiscountedPrice(double originalPrice, decimal discountPercentage)
        {
            double discountAmount = originalPrice * ((double)discountPercentage / 100);
            return originalPrice - discountAmount;
        }

        // POST: Promotions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PromotionId,StartDate,EndDate,DiscountProcent,ProductId")] Promotion promotion)
        {
            if (ModelState.IsValid)
            {
                var promotionDto = new PromotionCreateDTO
                {
                    StartDate = promotion.StartDate,
                    EndDate = promotion.EndDate,
                    DiscountProcent = promotion.DiscountProcent,
                    ProductId = promotion.ProductId
                    // Set other properties as needed
                };
                await _promotionService.CreateAsync<PromotionCreateDTO>(promotionDto);
                await _promotionService.SaveChangesAsync();

                var product = await _promotionService.GetProductByIdAsync<Product>(promotion.ProductId);
                if (product != null)
                {
                    double discountPrice = product.UnitPrice * (1 - promotion.DiscountProcent / 100);
                    product.DiscountUnitPrice = discountPrice;
                    product.Discount = true;
                    product.UnitPrice = discountPrice;
                    await _promotionService.UpdateProductAsync<ApiResponse>(product);


                }
                return RedirectToAction(nameof(Index));

            }
            List<Order> orderList = new();
            var orderResponse = _promotionService.GetProductsAsync<ApiResponse>();
            ViewData["ProductId"] = ("ProductId", "ProductName");
            return View();
        }


        // POST: Promotions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PromotionId,StartDate,EndDate,DiscountProcent,ProductId")] Promotion promotion)
        {
            if (id != promotion.PromotionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _promotionService.UpdatePromotionAsync<ApiResponse>(promotion);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PromotionExists(promotion.PromotionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
          //  ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "ProductId", promotion.ProductId);
            return View(promotion);
        }

        // GET: Promotions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promotion = await _promotionService.GetPromotionByIdAsync<Promotion>(id.Value);
            if (promotion == null)
            {
                return NotFound();
            }

            return View(promotion);
        }

        // POST: Promotions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var promotion = await _promotionService.GetPromotionByIdAsync<Promotion>(id);
            if (promotion == null)
            {
                return NotFound();
            }
            await _promotionService.DeletePromotionAsync<ApiResponse>(id);
            return RedirectToAction(nameof(Index));          
        }

        private bool PromotionExists(int id)
        {
            var promotion = _promotionService.GetPromotionByIdAsync<Promotion>(id);
            return promotion != null;
        }
    }
}
