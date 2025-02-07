using BankingApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BankingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BankingApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtTokenService _jwtTokenService;
        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, JwtTokenService jwtTokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        
        [HttpPost]
        [Route("api/login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        var token = _jwtTokenService.GenerateToken(user.UserName); // Generate JWT token
                        return Ok(new { Token = token }); // Return the token
                        //return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    {
                        return Unauthorized("Invalid credentials");
                        //ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    }
                }
                else
                {
                    return Unauthorized("Invalid credentials");
                    //ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }

            return BadRequest("Invalid login request");
            //return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }

            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Sign out the user
            await _signInManager.SignOutAsync();

            // Redirect to Login page
            return RedirectToAction("Login", "Account"); // or wherever your login page is
        }

    }
}
