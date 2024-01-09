using Newtonsoft.Json.Linq;
using PokeApi.Server.Models;
using PokeApi.Server.Services.Interfaces;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PokeApi.Server.Services.Classes
{
    public class PokemonService : IPokemonService
    {
        private const string POKEMON_API_URL = "https://pokeapi.co/api/v2/pokemon";

        private async Task<int> GetPokemonsCount()
        {
            int count = 0;

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(POKEMON_API_URL);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var pokeData = JObject.Parse(content);
                    count = int.Parse((string)pokeData["count"]);
                }
            }
            return count;
        }

        private int ParsePokemonId(string url)
        {
            var pattern = @"/(\d+)/$";
            var regex = new Regex(pattern);
            var match = regex.Match(url);

            if (match.Success)
                return int.Parse(match.Groups[1].Value);
            return -1;
        }

        public async Task<Pokemon> GetPokemonInfo(int id)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{POKEMON_API_URL}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var pokemonData = JObject.Parse(content);
                    var pokemon = new Pokemon()
                    {
                        Id = int.Parse((string)pokemonData["id"]),
                        Name = (string)pokemonData["name"],
                        Hp = int.Parse((string)pokemonData["stats"][0]["base_stat"]),
                        AttackPower = int.Parse((string)pokemonData["stats"][1]["base_stat"]),
                        ImageUrl = (string)pokemonData["sprites"]["other"]["official-artwork"]["front_default"]
                    };
                    return pokemon;
                }
                return null;
            }
        }

        public async Task<List<Pokemon>> GetPokemons(string startWith = "")
        {
            var pokemonsCount = await GetPokemonsCount();
            var pokemons = new List<Pokemon>();

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{POKEMON_API_URL}?limit={pokemonsCount}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var pokeData = JObject.Parse(content);
                    foreach (var poke in pokeData["results"])
                    {
                        pokemons.Add(new Pokemon()
                        {
                            Id = ParsePokemonId((string)poke["url"]),
                            Name = (string)poke["name"]
                        });
                    }
                }
            }
            return pokemons.Where(p => p.Name.StartsWith(startWith)).ToList();
        }
    }
}
