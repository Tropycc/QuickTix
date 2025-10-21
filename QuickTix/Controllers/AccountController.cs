using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace QuickTix.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;

        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: Account/Login
        public IActionResult Login()
        {
            return View();
        }
        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password, string returnUrl)
        {

            if (username == _configuration["ticketsusername"] && password == _configuration["ticketspassword"])
            {
                // Create a list of claims identifying the user
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, "admin"), // unique ID
                        new Claim(ClaimTypes.Name, "Administrator"), // human readable name
                        //new Claim(ClaimTypes.Role, "Smuggler"), // could use roles if needed         
                    };

                // Create the identity from the claims
                var claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                // Sign-in the user with the cookie authentication scheme
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            ViewBag.ErrorMessage = "Invalid username or password";
            return View();
        }

        // GET: Account/Logout
        public IActionResult Logout()
        {
            return View();
        }
        // POST: Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogoutConfirmed()
        {
            // Sign-out the user
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Account", "Login");
        }
    }
}
