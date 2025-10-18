using Company_MVC03.DAL.Data.Contexts;
using Company_MVC03.DAL.Models;
using Company_MVC03.PL.Controllers;
using Company_MVC03.PL.Dtos;
using Company_MVC03.PL.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        private readonly IMailServices _mailServices;


        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMailServices mailServices)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mailServices = mailServices;
        }

        #region SignUp
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        // P@ssW0rd

        public async Task<IActionResult> SignUp(SignUpDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var existingUser = await _userManager.Users
                .FirstOrDefaultAsync(u => u.UserName == model.UserName || u.Email == model.Email);

            if (existingUser is not null)
            {
                if (existingUser.UserName == model.UserName)
                    ModelState.AddModelError("", "Username already taken");

                if (existingUser.Email == model.Email)
                    ModelState.AddModelError("", "Email already taken");

                return View(model);
            }

            var user = new AppUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                IsAgree = model.IsAgree,
                UserName = model.UserName,
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
            return RedirectToAction("SignIn");
        }

        /*
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
        */
        #endregion

        #region SignIn
        [HttpGet]
        public IActionResult SignIn()
        {

            return View();
        }

        // P@ssW0rd


        [HttpPost]
        public async Task<IActionResult> SignIn(SignInDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                ModelState.AddModelError("", "Invalid login");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid login");
                return View(model);
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        /*
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
        */
        #endregion

        #region SignOut
        [HttpPost]
        public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn");
        }
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
                if (user is not null)
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
                    //var flag = Company_MVC03.PL.Helpers.EmailSettings.SendEmail(email);

                    //if (flag)
                    //{
                    //    // Check Your Inbox
                    //    return RedirectToAction("CheckYourInbox");
                    //}
                    _mailServices.SendEmail(email);
                    return RedirectToAction("CheckYourInbox");
                }
            }
            ModelState.AddModelError("", "Invalid Reset Password Url");
            return View("ForgetPassword", model);
        }

        [HttpGet]
        public IActionResult CheckYourInbox()
        {
            return View();
        }
        #endregion

        #region ResetPassword
        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            TempData["Email"] = email;
            TempData["Token"] = token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            if (ModelState.IsValid)
            {
                var email = TempData["email"] as string;
                var token = TempData["token"] as string;

                if (email is null || token is null) return BadRequest("Invalid Operation");
                var user = await _userManager.FindByEmailAsync(email);
                if (user is not null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, token, model.NwePassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("SignIn");
                    }
                }
                ModelState.AddModelError("", "Invalid Reset Password Operation");
            }
            return View();
        }
        #endregion

    }
}