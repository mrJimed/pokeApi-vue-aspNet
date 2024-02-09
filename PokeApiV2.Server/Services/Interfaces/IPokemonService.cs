using PokeApiV2.Server.Models;

namespace PokeApiV2.Server.Services.Interfaces
{
    public interface IPokemonService
    {
        Task<List<Pokemon>> GetPokemonsAsync(string startWith = "");
        Task<Pokemon?> GetPokemonInfoAsync(int id);
    }
}
