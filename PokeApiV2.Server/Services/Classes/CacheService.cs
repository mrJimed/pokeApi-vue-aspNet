using Microsoft.Extensions.Caching.Distributed;
using PokeApiV2.Server.Models;
using PokeApiV2.Server.Services.Interfaces;
using System.Text.Json;

namespace PokeApiV2.Server.Services.Classes
{
    public class CacheService(IDistributedCache redisCache) : ICacheService
    {
        public void AddPokemonToCache(Pokemon pokemon)
        {
            var pokemonJson = JsonSerializer.Serialize(pokemon);
            redisCache.SetString($"pokemon_{pokemon.Id}", pokemonJson, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            });
        }

        public Pokemon? GetPokemonFromCache(int id)
        {
            var pokemonJson = redisCache.GetString($"pokemon_{id}");
            if (pokemonJson != null)
                return JsonSerializer.Deserialize<Pokemon>(pokemonJson);
            return null;
        }
    }
}
