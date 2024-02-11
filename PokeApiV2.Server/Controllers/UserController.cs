using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PokeApiV2.Server.DbContexts;
using PokeApiV2.Server.Models;
using PokeApiV2.Server.Services.Interfaces;
using System.Security.Claims;

namespace PokeApiV2.Server.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController(PokeApiDbContext dbContext, IPasswordHelper passwordHelper) : ControllerBase
    {
        [HttpPost("registration")]
        public async Task<IActionResult> RegistrationAsync(User user)
        {
            if (dbContext.Users.FirstOrDefault(u => u.Email.Equals(user.Email)) != null)
                return Conflict("Пользователь с таким email уже существует");

            var hashedPasswordResult = passwordHelper.CreateHashPassword(user.Password);
            user.Password = hashedPasswordResult.Item1;
            user.Salt = Convert.ToBase64String(hashedPasswordResult.Item2);

            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            await SignInAsync(user.Email);
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(User user)
        {
            var fullUser = dbContext.Users.FirstOrDefault(u => u.Email.Equals(user.Email));

            if (fullUser == null)
                return BadRequest("Пользователя с таким email не существует");
            else if(!fullUser.Password.Equals(passwordHelper.CreateHashPassword(user.Password, Convert.FromBase64String(fullUser.Salt))))
                return BadRequest("Неправильный email или пароль");
            await SignInAsync(fullUser.Email);
            return Ok(fullUser);
        }

        [HttpPost("logout")]
        public async Task LogoutAsync()
        {
            await HttpContext.SignOutAsync();
        }

        private async Task SignInAsync(string email)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, email) };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }
    }
}
