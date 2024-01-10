using Microsoft.AspNetCore.Mvc;
using PokeApi.Server.Models;
using PokeApi.Server.Services.Interfaces;

namespace PokeApi.Server.Controllers
{
    [ApiController]
    [Route("pokemons")]
    public class PokemonsController : ControllerBase
    {
        private ILogger<PokemonsController> logger;
        private IPokemonService pokemonService;

        public PokemonsController([FromServices] IPokemonService pokemonService, [FromServices] ILogger<PokemonsController> logger)
        {
            this.pokemonService = pokemonService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Pokemon>> GetPokemonsAsync([FromQuery] string startWith = "")
        {
            var pokemons = await pokemonService.GetPokemonsAsync(startWith);
            logger.LogInformation($"Кол-во покемонов: {pokemons.Count}");
            return pokemons;
        }

        [HttpGet("{id:int}")]
        public async Task<Pokemon> GetPokemonInfoAsync(int id)
        {
            var pokemonInfo = await pokemonService.GetPokemonInfoAsync(id);
            logger.LogInformation(string.Format("Покемон с Id = {0} {1}", id, pokemonInfo != null ? "найден" : "не найден"));
            return pokemonInfo;
        }
    }
}
