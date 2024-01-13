using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using PokeApi.Server.Models;
using PokeApi.Server.Services.Interfaces;
using System.Text.Json;

namespace PokeApi.Server.Controllers
{
    [ApiController]
    [Route("pokemons")]
    public class PokemonsController : ControllerBase
    {
        private ILogger<PokemonsController> logger;
        private IPokemonService pokemonService;
        private IDistributedCache redisCache;

        public PokemonsController([FromServices] IPokemonService pokemonService, [FromServices] ILogger<PokemonsController> logger, [FromServices] IDistributedCache redisCache)
        {
            this.pokemonService = pokemonService;
            this.logger = logger;
            this.redisCache = redisCache;
        }

        [HttpGet]
        public async Task<IEnumerable<Pokemon>> GetPokemonsAsync([FromQuery] string startWith = "")
        {
            var pokemons = await pokemonService.GetPokemonsAsync(startWith);
            return pokemons;
        }

        [HttpGet("{id:int}")]
        public async Task<Pokemon?> GetPokemonInfoAsync(int id)
        {
            var pokemon = await GetPokemonFromCacheAsync(id);
            if(pokemon == null)
            {
                pokemon = await pokemonService.GetPokemonInfoAsync(id);
                await AddPokemonToCacheAsync(pokemon);
            }
            return pokemon;
        }

        [HttpGet("random")]
        public async Task<int> GetRandomPokemonIdAsync(int id)
        {
            var pokemons = await pokemonService.GetPokemonsAsync();
            var rnd = new Random();
            var randomIndex = rnd.Next(pokemons.Count);
            return pokemons[randomIndex].Id;
        }

        private async Task AddPokemonToCacheAsync(Pokemon pokemon)
        {
            var pokemonJson = JsonSerializer.Serialize(pokemon);
            await redisCache.SetStringAsync($"pokemon_{pokemon.Id}", pokemonJson, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            });
        }

        private async Task<Pokemon?> GetPokemonFromCacheAsync(int id)
        {
            var pokemonJson = await redisCache.GetStringAsync($"pokemon_{id}");
            if(pokemonJson != null)
                return JsonSerializer.Deserialize<Pokemon>(pokemonJson);
            return null;
        }
    }
}
