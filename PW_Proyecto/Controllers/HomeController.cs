using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PW_Proyecto.Models;
using PW_Proyecto.Models.Auth;
using System.Diagnostics;
using System.Security.Claims;

namespace PW_Proyecto.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [Route("/login")]
        [HttpPost]
        public async Task<IActionResult> Login(UserAuth credentials)
        {
            var user = await ApiService.Login(credentials);
            if (user == null)
            {
                return View(credentials);
            }
            var claims = new List<Claim>
            {
                new Claim("username", user.Username),
                new Claim("TokenAPI", user.Token)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(claimsPrincipal);
            ApiService.token = user.Token;
            return RedirectToAction("Index");
        }

        [Route("/register")]
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [Route("/register")]
        [HttpPost]
        public async Task<IActionResult> Register(User usuarionuevo)
        {
            var user = await ApiService.Register(usuarionuevo);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim("username", user.Username),
                    new Claim("TokenAPI", user.Token)
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                ApiService.token = user.Token;
                return RedirectToAction("Index");
            }
            return View(usuarionuevo);
        }

        [Route("/logout")]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }


        public IActionResult AboutUs()
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