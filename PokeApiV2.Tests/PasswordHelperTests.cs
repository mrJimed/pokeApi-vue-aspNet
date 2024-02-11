using PokeApiV2.Server.Services.Classes;
using PokeApiV2.Server.Services.Interfaces;
using Xunit;

namespace PokeApiV2.Tests
{
    public class PasswordHelperTests
    {
        private IPasswordHelper passwordHelper;

        public PasswordHelperTests()
        {
            passwordHelper = new PasswordHelper();
        }

        [Fact]
        public void CreateHashPasswordTest()
        {
            var password1 = "123456";
            var password2 = "password";
            var hashPasswordData = passwordHelper.CreateHashPassword(password1);

            Assert.Equal(hashPasswordData.Item1, passwordHelper.CreateHashPassword(password1, hashPasswordData.Item2));
            Assert.NotEqual(hashPasswordData.Item1, passwordHelper.CreateHashPassword(password2, hashPasswordData.Item2));
        }
    }
}
