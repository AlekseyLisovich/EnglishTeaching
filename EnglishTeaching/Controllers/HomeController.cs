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
        public HomeController()
        {
        }

        [Authorize(Roles = "admin, user")]
        public IActionResult Index()
        {
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
