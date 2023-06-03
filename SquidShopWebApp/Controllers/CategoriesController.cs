using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {

        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        //INDEX GET
        public async Task<IActionResult> Index()
        {
            List<Category> list = new();
            var response = await _categoryService.GetAllAsync<ApiResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<Category>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category model)
        {
            if (ModelState.IsValid)
            {
                var response = await _categoryService.CreateAsync<ApiResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
        //public async Task<SelectList> PopulateDropDown<T>(string type)
        //{
        //    var response = await _categoryService.GetAllAsync<ApiResponse>();
        //    var entities = JsonConvert.DeserializeObject<List<Category>>(Convert.ToString(response.Result));
        //    return new SelectList(entities, "CategoryId ", "CategoryId");
        //}
        //private readonly ApplicationDbContext _context;
    }
}
