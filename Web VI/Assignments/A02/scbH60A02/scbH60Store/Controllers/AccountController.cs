using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using scbH60Store.Models;
using Microsoft.AspNetCore.Authorization;

namespace scbH60Store.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    Province = model.Province,
                    CreditCard = model.CreditCard,
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Check or assign role based on logic or user input
                    string role = model.UserType == "Clerk" ? "Clerk" : "Customer";

                    // Assign the role to the newly created user
                    await _userManager.AddToRoleAsync(user, role);

                    // Sign in the user immediately after registration
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    // Redirect based on the role
                    if (role == "Clerk")
                    {
                        return RedirectToAction("ClerkDashboard", "Home"); // Adjust route as needed
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    // Redirect based on the user role after login
                    var user = await _userManager.FindByEmailAsync(model.Email);

                    if (await _userManager.IsInRoleAsync(user, "Manager"))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (await _userManager.IsInRoleAsync(user, "Clerk"))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError("", "Invalid login attempt.");
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
