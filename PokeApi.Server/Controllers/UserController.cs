using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PokeApi.Server.Models;
using PokeApi.Server.Services.Interfaces;
using System.Security.Claims;

namespace PokeApi.Server.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private ILogger<PokemonsController> logger;
        private IUserService userService;

        public UserController([FromServices] IUserService userService, [FromServices] ILogger<PokemonsController> logger)
        {
            this.userService = userService;
            this.logger = logger;
        }

        [HttpPost("login")]
        public async Task<User?> LoginAsync([FromBody] User loginUser)
        {
            var user = userService.LoginUser(loginUser);
            if (user != null)
                await SignInAsync(user.Email);
            return user;
        }

        [HttpPost("registration")]
        public async Task<User?> RegistrationAsync([FromBody] User newUser)
        {
            var user = userService.RegistrationUser(newUser);
            if (user != null)
                await SignInAsync(user.Email);
            return user;
        }

        [HttpPost("logout")]
        public async void LogoutAsync()
        {
            await HttpContext.SignOutAsync();
        }

        [HttpGet("current")]
        public User? Current()
        {
            var email = User.Identity.Name;
            return userService.GetUser(email);
        }

        private async Task SignInAsync(string email)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, email) };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }
    }
}
