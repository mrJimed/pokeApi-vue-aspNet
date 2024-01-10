using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using PokeApi.Server.DbContexts;
using PokeApi.Server.Models;
using PokeApi.Server.Services.Interfaces;
using System.Security.Cryptography;

namespace PokeApi.Server.Services.Classes
{
    public class UserService : IUserService
    {
        private PokeApiDbContext pokeDbContext;

        public UserService([FromServices] PokeApiDbContext pokeDbContext)
        {
            this.pokeDbContext = pokeDbContext;   
        }

        public User? GetUser(string email)
        {
            return pokeDbContext.Users.FirstOrDefault(user => email.Equals(user.Email));
        }

        public User? RegistrationUser(User newUser)
        {
            if (!IsUserExists(newUser.Email))
            {
                var salt = RandomNumberGenerator.GetBytes(16);
                var hashPassword = CreateHashPassword(newUser.Password, salt);

                newUser.Salt = Convert.ToBase64String(salt);
                newUser.Password = hashPassword;

                pokeDbContext.Users.Add(newUser);
                pokeDbContext.SaveChanges();
                return newUser;
            }
            return null;
        }

        public User? LoginUser(User loginUser)
        {
            if (IsUserExists(loginUser.Email))
            {
                var user = pokeDbContext.Users.First(user => user.Email.Equals(loginUser.Email));
                var salt = Convert.FromBase64String(user.Salt);
                var hashPassword = CreateHashPassword(loginUser.Password, salt);
                return user.Password.Equals(hashPassword) ? user : null;
            }
            return null;
        }

        public bool IsUserExists(string email)
        {
            return pokeDbContext.Users.FirstOrDefault(user => email.Equals(user.Email)) != null;
        }

        private string CreateHashPassword(string password, byte[] salt)
        {
            var hashedPassword = Convert.ToBase64String(
            KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 32));
            return hashedPassword;
        }
    }
}
