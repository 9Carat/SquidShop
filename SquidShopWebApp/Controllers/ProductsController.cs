using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SquidShopWebApp.Data;
using SquidShopWebApp.Models;
using SquidShopWebApp.Models.DTO;
using SquidShopWebApp.Services.IServices;


namespace SquidShopWebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }
        //GET
        public async Task<IActionResult> Index()
        {
            List<Product> list = new();
            var response = await _productService.GetAllAsync<ApiResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<Product>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
        //Populates list of categories
        public async Task<SelectList> PopulateDropDown<T>()
        {
            var response = await _productService.GetList<ApiResponse>();
            var entities = JsonConvert.DeserializeObject<List<Category>>(Convert.ToString(response.Result));
            return new SelectList(entities, "CategoryId", "CategoryName");
        }

        public IActionResult BuyProduct(int id)
        {
            var productViewModel = new ProductViewModel();
            productViewModel.ProductId = id;
            return View(productViewModel);
        }

        //GET POST
        public IActionResult Create()
		{
            //   ViewBag.Category = PopulateDropDown<Category>().GetAwaiter().GetResult();
			return View();
        }
        //POST CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.CreateAsync<ApiResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model); 
        }
        //GET UPDATE
        public async Task<IActionResult> UpdateProduct(int productId)
        {
            var response = await _productService.GetByIdAsync<ApiResponse>(productId);
            if (response != null && response.IsSuccess)
            {
                Product model = JsonConvert.DeserializeObject<Product>(Convert.ToString(response.Result));
                return View(_mapper.Map<ProductUpdateDTO>(model));
            }
            return NotFound();
        }
        //POST UPDATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProduct(ProductUpdateDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.UpdateAsync<ApiResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
        // GET DELETE
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var response = await _productService.GetByIdAsync<ApiResponse>(productId);
            if (response != null && response.IsSuccess)
            {
                Product model = JsonConvert.DeserializeObject<Product>(Convert.ToString(response.Result));
                return View(model); 
            }
            return NotFound();
        }
        //POST DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProduct(Product model)
        {
            var response = await _productService.DeleteAsync<ApiResponse>(model.ProductId);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }



        //private readonly ApplicationDbContext _context;
        //private readonly IWebHostEnvironment _hostEnvironment;

        //public ProductsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        //{
        //    _context = context;
        //    _hostEnvironment = hostEnvironment;
        //}

        //// GET: Products
        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.Products.Include(p => p.Categories);
        //    return View(await applicationDbContext.ToListAsync());
        //}

        //public IActionResult BuyProduct(int id)
        //{
        //    var productViewModel = new ProductViewModel();
        //    productViewModel.ProductId = id;
        //    return View(productViewModel);
        //}

        //// GET: Products/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.Products == null)
        //    {
        //        return NotFound();
        //    }

        //    var product = await _context.Products
        //        .Include(p => p.Categories)
        //        .FirstOrDefaultAsync(m => m.ProductId == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(product);
        //}

        //// GET: Products/Create
        //public IActionResult Create()
        //{
        //    ViewData["FK_CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
        //    return View();
        //}

        //// POST: Products/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ProductId,ProductName,Stock,UnitPrice,Discount,DiscountPrice,ImageFile,FK_CategoryId")] Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Sparar bild i wwwroot/images med unikt filnamn
        //        string rootPath = _hostEnvironment.WebRootPath;
        //        string fileName = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
        //        string extension = Path.GetExtension(product.ImageFile.FileName);
        //        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
        //        product.ImageName = fileName;
        //        string path = Path.Combine(rootPath + "/Images/", fileName);
        //        using (var fileStream = new FileStream(path, FileMode.Create))
        //        {
        //            await product.ImageFile.CopyToAsync(fileStream);
        //        }

        //        // Lägger in övriga props
        //        _context.Add(product);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["FK_CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.FK_CategoryId);
        //    return View(product);
        //}

        //// GET: Products/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Products == null)
        //    {
        //        return NotFound();
        //    }

        //    var product = await _context.Products.FindAsync(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["FK_CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.FK_CategoryId);
        //    return View(product);
        //}

        //// POST: Products/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,Stock,UnitPrice,Discount,DiscountPrice,ImageName,FK_CategoryId")] Product product)
        //{
        //    if (id != product.ProductId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(product);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ProductExists(product.ProductId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["FK_CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.FK_CategoryId);
        //    return View(product);
        //}

        //// GET: Products/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Products == null)
        //    {
        //        return NotFound();
        //    }

        //    var product = await _context.Products
        //        .Include(p => p.Categories)
        //        .FirstOrDefaultAsync(m => m.ProductId == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(product);
        //}

        //// POST: Products/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Products == null)
        //    {
        //        return Problem("Entity set 'ApplicationDbContext.Products'  is null.");
        //    }
        //    var product = await _context.Products.FindAsync(id);

        //    // Tar bort bild från wwwroot/images
        //    var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "images", product.ImageName);
        //    if (System.IO.File.Exists(imagePath))
        //    {
        //        System.IO.File.Delete(imagePath);
        //    }

        //    if (product != null)
        //    {
        //        _context.Products.Remove(product);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool ProductExists(int id)
        //{
        //    return _context.Products.Any(e => e.ProductId == id);
        //}
    }
    }
