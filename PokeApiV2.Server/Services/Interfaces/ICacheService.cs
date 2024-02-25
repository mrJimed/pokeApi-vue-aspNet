using PokeApiV2.Server.Models;

namespace PokeApiV2.Server.Services.Interfaces
{
    public interface ICacheService
    {
        public void AddPokemonToCache(Pokemon pokemon);
        public Pokemon? GetPokemonFromCache(int id);
    }
}
