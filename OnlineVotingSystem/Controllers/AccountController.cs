using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineVotingSystem.Models.Interfaces;
using OnlineVotingSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineVotingSystem.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        [Route("register")]
        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated && !String.IsNullOrEmpty(User.Identity.Name))
            {
                return (User.IsInRole("Admin")) ? RedirectToAction("", "admin") : RedirectToAction("", "voter");
            }

            return View();
        }

        [Route("register")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (await _accountService.IsUsernameExist(viewModel.Username))
                    {
                        TempData["errorMessage"] = "This username is already taken. Please use a different username.";
                        return View(viewModel);
                    }

                    await _accountService.Register(viewModel);
                    TempData["successMessage"] = "Successfully Created";
                    return RedirectToAction("login", "account");
                }
                catch (Exception ex)
                {
                    TempData["errorMessage"] = "An error has occured in Register(). Error Message: " + ex.Message;
                }
            }

            return View(viewModel);
        }

        [Route("login")]
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated && !String.IsNullOrEmpty(User.Identity.Name))
            {
                return (User.IsInRole("Admin")) ? RedirectToAction("dashboard", "admin") : RedirectToAction("election-list", "voter");
            }

            return View();
        }


        [Route("login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (await _accountService.Login(viewModel))
                    {
                        var claims = new List<Claim> { new Claim(ClaimTypes.Name, viewModel.Username), new Claim(ClaimTypes.Role, await _accountService.GetUserHighestRoleName(viewModel.Username)) };
                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var authProperties = new AuthenticationProperties { IsPersistent = viewModel.RememberMe };
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                        return (await _accountService.IsAdmin(viewModel.Username)) ? RedirectToAction("dashboard", "admin") : RedirectToAction("election-list", "voter");
                    }
                    TempData["errorMessage"] = "Incorrect Username and/or Password";
                    return RedirectToAction("login", "account");
                }
                catch (Exception ex)
                {
                    TempData["errorMessage"] = "An error has occured in Login(). Error Message: " + ex.Message;
                }
            }

            return View(viewModel);
        }


        [Route("logout")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("login", "account");
        }


    }
}
