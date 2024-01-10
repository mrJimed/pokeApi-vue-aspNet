using PokeApi.Server.Models;

namespace PokeApi.Server.Services.Interfaces
{
    public interface IUserService
    {
        bool IsUserExists(string email);
        public User? RegistrationUser(User newUser);
        User? LoginUser(User loginUser);
        User? GetUser(string email);
    }
}
