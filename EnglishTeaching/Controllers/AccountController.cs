using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLayer;
using BusinessLayer.Interfaces;
using DataLayer.Entities.Account;
using EnglishTeaching.Models.Account;
using EnglishTeaching.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace EnglishTeaching.Controllers
{
    public class AccountController : Controller
    {
        AccountService _accountService;

        public AccountController(IAsyncRepository<User> userRepository, IAsyncRepository<Role> roleRepository)
        {
            _accountService = new AccountService(userRepository, roleRepository);
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
                User user = await _accountService.RegisterUser(model);

                await Authenticate(user);

                return RedirectToAction("Index", "Home");
				//hello world
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
                User user = await _accountService.LoginUser(model);

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
            var model = await _accountService.GetUserProfile(User.Identity.Name);

            return View(model);
        }

        [HttpPost]
        public IActionResult MyProfile(UserProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                _accountService.SaveUserProfile(model);

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