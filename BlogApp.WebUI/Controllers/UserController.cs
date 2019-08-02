using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Entity;
using ProjectManagement.WebUI.Models;

namespace ProjectManagement.WebUI.Controllers
{
    public class UserController : Controller
    {

        private UserManager<ApplicationUser> userManager;

        public UserController(UserManager<ApplicationUser> _userManager)
        {
            userManager = _userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult List()
        {
            return View(userManager.Users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Create(UserRegisterModel user)
        {

            if (ModelState.IsValid)
            {

                ApplicationUser appuser = new ApplicationUser();
                appuser.UserName = user.UserName;
                appuser.Email = user.Email;
                appuser.Name = user.Name;
                appuser.Surname = user.Surname;


                var result = await userManager.CreateAsync(appuser, user.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }

            }
            return View(user);
        }

    }
}