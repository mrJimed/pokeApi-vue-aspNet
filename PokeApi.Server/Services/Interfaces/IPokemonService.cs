using PokeApi.Server.Models;

namespace PokeApi.Server.Services.Interfaces
{
    public interface IPokemonService
    {
        public Task<List<Pokemon>> GetPokemonsAsync(string startWith = "");
        public Task<Pokemon> GetPokemonInfoAsync(int id);
    }
}
