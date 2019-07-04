using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EnglishTeaching.Models;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using BusinessLayer;
using DataLayer.Entities.Account;
using System.Threading.Tasks;

namespace EnglishTeaching.Controllers
{
    public class HomeController : Controller
    {
        private LoginManager _loginManager;
        public HomeController(LoginManager loginManager)
        {
            _loginManager = loginManager;
        }

        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> Index()
        {
            //var phrases = await _loginManager.UsersRepository.GetAll();
            return View();
        }

        [Authorize(Roles = "admin")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
