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
using SquidShopWebApp.Models;
using SquidShopWebApp.Models.DTO;
using SquidShopWebApp.Services.IServices;


namespace SquidShopWebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;
        public ProductsController(IProductService productService, IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            _productService = productService;
            _webHostEnvironment = webHostEnvironment;
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
        //GET POST
        public IActionResult Create()
		{
            ViewBag.Category = PopulateDropDown<Category>().GetAwaiter().GetResult();
			return View();
        }
        //POST CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                // Sparar bild i wwwroot/images med unikt filnamn
                string rootPath = _webHostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
                string extenstion = Path.GetExtension(model.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extenstion;
                model.ImageName = fileName;
                string path = Path.Combine(rootPath + "/Images", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(fileStream);
                }
                if (model.Discount != 0)
                {
                    var discount = Decimal.ToDouble(model.Discount);
                    var discountPrice = model.UnitPrice * (1 - discount);
                    model.DiscountUnitPrice = double.Floor(discountPrice);
                }
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
            ViewBag.Category = PopulateDropDown<Category>().GetAwaiter().GetResult();
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
                if (model.Discount != 0)
                {
                    var discount = Decimal.ToDouble(model.Discount);
                    var discountPrice = model.UnitPrice * (1 - discount);
                    model.DiscountUnitPrice = double.Floor(discountPrice);
                }
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
            var imageResponse = await _productService.GetByIdAsync<ApiResponse>(model.ProductId);
            if (imageResponse != null && imageResponse.IsSuccess)
            {
                Product imageModel = JsonConvert.DeserializeObject<Product>(Convert.ToString(imageResponse.Result));
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", imageModel.ImageName);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            var response = await _productService.DeleteAsync<ApiResponse>(model.ProductId);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        }
    }
