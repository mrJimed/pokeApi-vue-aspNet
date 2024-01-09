using PokeApi.Server.Models;

namespace PokeApi.Server.Services.Interfaces
{
    public interface IPokemonService
    {
        public Task<List<Pokemon>> GetPokemons(string startWith);
        public Task<Pokemon> GetPokemonInfo(int id);
    }
}
