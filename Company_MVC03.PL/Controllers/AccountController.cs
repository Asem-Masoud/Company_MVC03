using Company_MVC03.DAL.Data.Contexts;
using Company_MVC03.DAL.Models;
using Company_MVC03.PL.Controllers;
using Company_MVC03.PL.Dtos;
using Company_MVC03.PL.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using System.Security.Policy;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Compnay.C44.G02.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        #region SignUp
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }



        // P@ssW0rd
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDto model)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user is null)
                {
                    user = await _userManager.FindByEmailAsync(model.Email);

                    if (user is null)
                    {
                        // Register

                        user = new AppUser()
                        {
                            UserName = model.UserName,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Email = model.Email,
                            IsAgree = model.IsAgree,
                        };

                        var result = await _userManager.CreateAsync(user, model.Password);

                        if (result.Succeeded)
                        {
                            // Send Email To Confirm Email
                            return RedirectToAction("SignIn");
                        }

                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                ModelState.AddModelError("", "Invalid SignUp !!");
            }
            return View(model);
        }
        #endregion


        #region SignIn

        [HttpGet]
        public IActionResult SignIn()
        {

            return View();
        }

        // P@ssW0rd

        [HttpPost] // Account/SignIn
        public async Task<IActionResult> SignIn(SignInDto model)
        {
            if (!ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var flag = await _userManager.CheckPasswordAsync(user, model.Password);
                    if (flag)
                    {
                        // signIn
                        var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction(nameof(HomeController.Index), "Home");
                        }
                    }
                }
                ModelState.AddModelError("", "Invalid login");
            }
            return View(model);
        }


        #endregion

        #region SignOut
        /*
        [HttpPost]
        public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn");
        }
        */
        #endregion


        #region Forget Password

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendResetPasswordUrl(ForgetPasswordDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is null)
                {
                    // Generate Token
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user); // Add Generate In Program

                    // Create URL
                    var url = Url.Action("ResetPassword", "Account", new { email = model.Email, token }, Request.Scheme);

                    // Create Email
                    var email = new Email()
                    {
                        To = model.Email,
                        Subject = "Reset Password",
                        Body = url
                    };

                    // Send email 
                    var flag = Company_MVC03.PL.Helpers.EmailSettings.SendEmail(email);

                    if (flag)
                    {
                        // Check Your Inbox

                    }

                }

            }
            ModelState.AddModelError("", "Invalid Reset Password Url");

            return View("ForgetPassword", model);
        }


        #endregion

    }
}
