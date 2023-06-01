using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SquidShopWebApp.Models;
using System.Data;

namespace SquidShopWebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _usermanager;
        public AdminController(UserManager<IdentityUser> usermanager)
        {
            _usermanager = usermanager;
        }
        public async Task<IActionResult> Index()
        {
            var admins = (await _usermanager
                .GetUsersInRoleAsync("Admin"))
                .ToArray();

            var customers = await _usermanager.Users.ToArrayAsync();

            var model = new AdminViewModel
            {
                Admin = admins,
                Customer = customers,
            };

            return View(model);
        }
    }
}