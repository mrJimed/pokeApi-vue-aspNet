using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using PokeApiV2.Server.Models;
using PokeApiV2.Server.Services.Interfaces;
using System.Security.Cryptography;

namespace PokeApiV2.Server.Services.Classes
{
    public class PasswordHelper : IPasswordHelper
    {
        public Tuple<string, byte[]> CreateHashPassword(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(16);
            var hashedPassword = CreateHashPassword(password, salt);
            return new Tuple<string, byte[]>(hashedPassword, salt);
        }

        public string CreateHashPassword(string password, byte[] salt)
        {
            return Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA512,
                    iterationCount: 10000,
                    numBytesRequested: 32

                ));
        }
    }
}
