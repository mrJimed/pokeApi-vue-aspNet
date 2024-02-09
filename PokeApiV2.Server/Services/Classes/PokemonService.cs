using Newtonsoft.Json.Linq;
using PokeApiV2.Server.Models;
using PokeApiV2.Server.Services.Interfaces;
using System.Text.RegularExpressions;

namespace PokeApiV2.Server.Services.Classes
{
    public class PokemonService : IPokemonService
    {
        private const string API_URL = "https://pokeapi.co/api/v2/pokemon";

        public async Task<List<Pokemon>> GetPokemonsAsync(string startWith = "")
        {
            var pokemonsCount = await GetPokemonsCountAsync();
            var pokemons = new List<Pokemon>();

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{API_URL}?limit={pokemonsCount}");
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

        public async Task<Pokemon?> GetPokemonInfoAsync(int id)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{API_URL}/{id}");
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

        private async Task<int> GetPokemonsCountAsync()
        {
            int count = 0;

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(API_URL);
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
    }
}
