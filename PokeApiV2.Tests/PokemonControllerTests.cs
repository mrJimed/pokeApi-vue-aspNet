using Microsoft.AspNetCore.Mvc;
using Moq;
using PokeApiV2.Server.Controllers;
using PokeApiV2.Server.Models;
using PokeApiV2.Server.Services.Classes;
using PokeApiV2.Server.Services.Interfaces;
using Xunit;

namespace PokeApiV2.Tests
{
    public class PokemonControllerTests
    {
        private PokemonController pokemonController;

        public PokemonControllerTests()
        {
            var cacheServiceMock = new Mock<ICacheService>();
            pokemonController = new PokemonController(new PokemonService(), cacheServiceMock.Object);
        }

        [Fact]
        public async Task GetAllPokemonsAsyncTestAsync()
        {
            var pokemons = await pokemonController.GetAllPokemonsAsync();
            Assert.NotEmpty(pokemons);

            pokemons = await pokemonController.GetAllPokemonsAsync("asc");
            Assert.NotEmpty(pokemons);
            Assert.Equal(pokemons, pokemons.OrderBy(p => p.Name));

            pokemons = await pokemonController.GetAllPokemonsAsync("desc");
            Assert.NotEmpty(pokemons);
            Assert.Equal(pokemons, pokemons.OrderByDescending(p => p.Name));
        }

        [Fact]
        public async Task GetPokemonInfoAsyncTestAsync()
        {
            int exsistsId = 1;
            int notExistsId = -1;

            var result = await pokemonController.GetPokemonInfoAsync(exsistsId);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<Pokemon>((result as OkObjectResult).Value);

            result = await pokemonController.GetPokemonInfoAsync(notExistsId);
            Assert.NotNull(result);
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}