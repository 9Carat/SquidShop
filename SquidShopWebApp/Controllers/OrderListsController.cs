using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SquidShopWebApp.Data;
using SquidShopWebApp.Models;

namespace SquidShopWebApp.Controllers
{
    public class OrderListsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderListsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OrderLists
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.OrderLists.Include(o => o.Order).Include(o => o.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: OrderLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrderLists == null)
            {
                return NotFound();
            }

            var orderList = await _context.OrderLists
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderListId == id);
            if (orderList == null)
            {
                return NotFound();
            }

            return View(orderList);
        }

        // GET: OrderLists/Create
        public IActionResult Create()
        {
            ViewData["Fk_OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId");
            ViewData["FK_ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId");
            return View();
        }

        // POST: OrderLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderListId,FK_ProductId,Fk_OrderId,Price,Quantity")] OrderList orderList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Fk_OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderList.Fk_OrderId);
            ViewData["FK_ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", orderList.FK_ProductId);
            return View(orderList);
        }

        // GET: OrderLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrderLists == null)
            {
                return NotFound();
            }

            var orderList = await _context.OrderLists.FindAsync(id);
            if (orderList == null)
            {
                return NotFound();
            }
            ViewData["Fk_OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderList.Fk_OrderId);
            ViewData["FK_ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", orderList.FK_ProductId);
            return View(orderList);
        }

        // POST: OrderLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderListId,FK_ProductId,Fk_OrderId,Price,Quantity")] OrderList orderList)
        {
            if (id != orderList.OrderListId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderListExists(orderList.OrderListId))
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
            ViewData["Fk_OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderList.Fk_OrderId);
            ViewData["FK_ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", orderList.FK_ProductId);
            return View(orderList);
        }

        // GET: OrderLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrderLists == null)
            {
                return NotFound();
            }

            var orderList = await _context.OrderLists
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderListId == id);
            if (orderList == null)
            {
                return NotFound();
            }

            return View(orderList);
        }

        // POST: OrderLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrderLists == null)
            {
                return Problem("Entity set 'ApplicationDbContext.OrderLists'  is null.");
            }
            var orderList = await _context.OrderLists.FindAsync(id);
            if (orderList != null)
            {
                _context.OrderLists.Remove(orderList);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderListExists(int id)
        {
          return _context.OrderLists.Any(e => e.OrderListId == id);
        }
    }
}
