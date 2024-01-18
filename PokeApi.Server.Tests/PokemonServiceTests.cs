using PokeApi.Server.Services.Classes;
using PokeApi.Server.Services.Interfaces;
using Xunit;

namespace PokeApi.Server.Tests
{
    public class PokemonServiceTests
    {
        private IPokemonService pokemonService;

        public PokemonServiceTests()
        {
            pokemonService = new PokemonService();
        }

        [Fact]
        public async Task GetPokemonsAsyncTestAsync()
        {
            var pokemons = await pokemonService.GetPokemonsAsync();
            Assert.NotNull(pokemons);
        }

        [Fact]
        public async Task GetPokemonInfoAsyncTestAsync()
        {
            var pokemon = await pokemonService.GetPokemonInfoAsync(1);
            Assert.NotNull(pokemon);
            Assert.Equal(1, pokemon.Id);

            pokemon = await pokemonService.GetPokemonInfoAsync(-1);
            Assert.Null(pokemon);
        }
    }
}