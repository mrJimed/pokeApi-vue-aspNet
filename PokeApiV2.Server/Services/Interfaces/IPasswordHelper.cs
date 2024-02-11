namespace PokeApiV2.Server.Services.Interfaces
{
    public interface IPasswordHelper
    {
        Tuple<string, byte[]> CreateHashPassword(string password);
        string CreateHashPassword(string password, byte[] salt);
    }
}
