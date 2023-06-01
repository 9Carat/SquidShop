using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SquidShopWebApp.Data;
using SquidShopWebApp.Models;
using SquidShopWebApp.Services;
using SquidShopWebApp.Services.IServices;
using SquidShopWebApp.Models.DTO;

namespace SquidShopWebApp.Controllers
{
    public class PromotionsController : Controller
    {
        private readonly IPromotionService _promotionService;

        public PromotionsController(IPromotionService promotionService, IProductService productService)
        {
            _promotionService = promotionService;

            //_context = context;
        }

        // GET: Promotions
        public async Task<IActionResult> Index()
        {
            List<Promotion> list = new();
            var response = await _promotionService.GetAllAsync<ApiResponse>();
            if (response != null && response.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<List<Promotion>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        //// GET: Promotions/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    //if (id == null || _context.Promotions == null)
        //    //{
        //    //    return NotFound();
        //    //}

        //    //var promotion = await _context.Promotions
        //    //    .Include(p => p.Product)
        //    //    .FirstOrDefaultAsync(m => m.PromotionId == id);
        //    //if (promotion == null)
        //    //{
        //    //    return NotFound();
        //    //}

        //    //return View(promotion);
        //}

        // GET: Promotions/Create
        public async Task<IActionResult> Create()
        {
            var promotions = await _promotionService.GetAllAsync<IEnumerable<Promotion>>();
            if (promotions == null)
            {
                return NotFound();
            }
            var productIds = promotions.Select(p => p.ProductId).ToList();
            ViewData["ProductId"] = new SelectList(productIds, "ProductId", "ProductId");
            {
                return View();
            }
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
                    await _promotionService.CreateAsync<Promotion>(promotion);
                    await _promotionService.SaveChangesAsync();

                    var product = await _promotionService.GetByIdAsync<Product>(promotion.ProductId);
                    if (product != null)
                    {
                        double discountPrice = product.UnitPrice * (1 - promotion.DiscountProcent / 100);
                        product.DiscountPrice = (decimal)discountPrice;
                        product.Discount = true;
                        product.UnitPrice = discountPrice;
                        await _promotionService.UpdateAsync<Product>(promotion);
                        await _promotionService.SaveChangesAsync();

                    }
                    return RedirectToAction(nameof(Index));

                }
                var products = await _promotionService.GetAllAsync<IEnumerable<Product>>();
                ViewData["ProductId"] = new SelectList(products, "ProductId", "ProductId", promotion.ProductId);
                return View(promotion);
            }

            //// GET: Promotions/Edit/5
            //async Task<IActionResult> Edit(int? id)
            //{
            //    if (id == null || _promotionService == null)/////jsdnfojsdnåpf
            //    {
            //        return NotFound();
            //    }

            //    var promotion = await _promotionService.GetByIdAsync<Product>((int)id);
            //    if (promotion == null)
            //    {
            //        return NotFound();
            //    }
            //    var products = await _promotionService.GetAllAsync<IEnumerable<Product>>();
            //    ViewData["ProductId"] = new SelectList(products, "ProductId", "ProductId", promotion.ProductId);
            //    return View(promotion);
            //}

            // POST: Promotions/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            async Task<IActionResult> Edit(int id, [Bind("PromotionId,StartDate,EndDate,DiscountProcent,ProductId")] Promotion promotion)
            {
                if (id != promotion.PromotionId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        await _promotionService.UpdateAsync<Promotion>(promotion);
                        await _promotionService.SaveChangesAsync();
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
                var promotions = await _promotionService.GetAllAsync<IEnumerable<Promotion>>();
                ViewData["ProductId"] = new SelectList(promotions, "ProductId", "ProductId", promotion.ProductId);
                return View(promotion);
            }

            // GET: Promotions/Delete/5
            async Task<IActionResult> Delete(int? id)
            {
                if (id == null || _promotionService == null)
                {
                    return NotFound();
                }

                var promotion = await _promotionService
                .GetPromotionsIncludingProducts()
                .FirstOrDefaultAsync(m => m.PromotionId == id);
                if (promotion == null)
                {
                    return NotFound();
                }

                return View(promotion);
            }

            // POST: Promotions/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            async Task<IActionResult> DeleteConfirmed(int id)
            {

                if (_promotionService == null)
                {
                    return Problem("Entity set 'ApplicationDbContext.Promotions'  is null.");
                }

                var promotion = await _promotionService.GetByIdAsync<Promotion>(id);
                if (promotion != null)
                {
                    await _promotionService.DeleteAsync<ApiResponse>(promotion);

                }

                await _promotionService.GetAllAsync<IEnumerable<Promotion>>(); // Ange typargumentet explicit som IEnumerable<Promotion>
                return RedirectToAction(nameof(Index));
            }

            bool PromotionExists(int id)
            {
                return _promotionService.Any(e => ((Promotion)e).PromotionId == id);
            }
        }
    }

