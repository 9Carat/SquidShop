using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SquidShopWebApp.Models;
using SquidShopWebApp.Models.DTO;
using SquidShopWebApp.Services.IServices;

namespace SquidShopWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _user;
        public UserController(IUserService userService, IMapper mapper, UserManager<IdentityUser> user)
        {
            _userService = userService;
            _mapper = mapper;
            _user = user;
        }
        public async Task<IActionResult> Index()
        {
            List<User> list = new();
            var response = await _userService.GetAllAsync<ApiResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<User>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
        //public IActionResult Create()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(UserCreateDTO model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var userId = await _user.GetUserAsync(User);
        //        model.FK_UsersId = userId.Id;
        //        var response = await _userService.CreateAsync<ApiResponse>(model);
        //        if (response != null && response.IsSuccess)
        //        {
        //            return RedirectToAction(nameof(Create));
        //        }
        //    }
        //    return View(model);
        //}
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            int a = 2;
            var response = await _userService.GetByIdAsync<ApiResponse>(a);
            if (response != null && response.IsSuccess)
            {
                User model = JsonConvert.DeserializeObject<User>(Convert.ToString(response.Result));
                return View(_mapper.Map<UserUpdateDTO>(model));
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UserUpdateDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _userService.UpdateAsync<ApiResponse>(model);
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
            var response = await _userService.GetByIdAsync<ApiResponse>(interestid);
            if (response != null && response.IsSuccess)
            {
                User model = JsonConvert.DeserializeObject<User>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(User model)
        {
            if (ModelState.IsValid)
            {
                var response = await _userService.DeleteAsync<ApiResponse>(model.UserId);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }
    }
}