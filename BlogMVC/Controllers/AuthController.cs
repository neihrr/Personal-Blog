using BlogMVC.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogMVC.Controllers
{
    public class AuthController : Controller
    {
        private SignInManager<IdentityUser> _signInManager;
        private UserManager<IdentityUser> _usrManager;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> UserManager)
        {
           _signInManager = signInManager;
            _usrManager = UserManager;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View(new LogInViewModel());
        }


        [HttpPost]
        public async Task<IActionResult> Login(LogInViewModel vm)
        {
            var result = await _signInManager.PasswordSignInAsync(vm.UserName, vm.Password, false, false);
            
            if (!result.Succeeded)
            {
                return View(vm);
            }

            var user = await _usrManager.FindByNameAsync(vm.UserName);

            var isAdmin = await _usrManager.IsInRoleAsync(user, "Admin");

            if (isAdmin)
            {
                return RedirectToAction("Index", "Panel");
            }

            return RedirectToAction("Index", "Home"); 
           



            /*if (isAdmin)
            {
                return RedirectToAction("Index", "Panel");
            }


            return RedirectToAction("Post", "Home");
        }*/

        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    } 
}
