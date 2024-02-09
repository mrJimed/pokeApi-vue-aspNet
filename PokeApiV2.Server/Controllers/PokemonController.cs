using Microsoft.AspNetCore.Mvc;
using PokeApiV2.Server.Models;
using PokeApiV2.Server.Services.Interfaces;

namespace PokeApiV2.Server.Controllers
{
    [Route("pokemons")]
    [ApiController]
    public class PokemonController(IPokemonService pokemonService) : ControllerBase
    {
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPokemonInfoAsync(int id)
        {
            var pokemon = await pokemonService.GetPokemonInfoAsync(id);
            if (pokemon == null)
                return NotFound($"Покемон с Id = {id} не найден");
            return Ok(pokemon);
        }

        [HttpGet]
        public async Task<List<Pokemon>> GetAllPokemonsAsync()
        {
            var pokemons = await pokemonService.GetPokemonsAsync();
            return pokemons;
        }
    }
}
