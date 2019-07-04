using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLayer;
using DataLayer.Entities.Account;
using EnglishTeaching.Models.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PresentationLayer;
using PresentationLayer.Models.Account;

namespace EnglishTeaching.Controllers
{
    public class AccountController : Controller
    {
        private LoginManager _loginManager;
        private ServicesManager _servicesmanager;

        public AccountController(LoginManager loginManager)
        {
            _loginManager = loginManager;
            _servicesmanager = new ServicesManager(loginManager);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _servicesmanager.AccountService.RegisterUser(model);

                await Authenticate(user);

                return RedirectToAction("Index", "Home");
            }
            else
                ModelState.AddModelError("", "Incorrect login or password");
        
            return View(model);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _servicesmanager.AccountService.LoginUser(model);

                if (user != null)
                {
                    await Authenticate(user);

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Incorrect login or password");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> MyProfile()
        {
            //UserProfile user = await _context.UserProfiles.FirstOrDefaultAsync(u => u.EmailAddress == User.Identity.Name);

            //UserProfileModel profileModel = new UserProfileModel
            //{
            //    Id = user.Id,
            //    Email = user.EmailAddress,
            //    Name = user.Name,
            //    Age = user.Age,
            //    CellPhone = user.CellPhone,
            //    Company = user.Company
            //};

            return View(/*profileModel*/);
        }

        [HttpPost]
        public async Task<IActionResult> MyProfile(UserProfileModel model)
        {
            if (ModelState.IsValid)
            {
                //UserProfile profile = await _context.UserProfiles.FirstOrDefaultAsync(p => p.Id == model.Id);

                //if(profile != null)
                //{
                //    profile.EmailAddress = model.Email;
                //    profile.Name = model.Name;
                //    profile.Age = model.Age;
                //    profile.CellPhone = model.CellPhone;
                //    profile.Company = model.Company;

                //    await _context.SaveChangesAsync();
                //}

                return View();
            } 
            else
            {
                ModelState.AddModelError("", "Incorrect some data");
                return View(model);
            }
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}