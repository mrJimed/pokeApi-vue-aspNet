import axios from "axios";

export async function getPokemons(startWith = "") {
    try {
        const { data } = await axios.get(`/pokemons?startWith=${startWith}`);
        return data;
    } catch (err) {
        console.log(err);
    }
}

export async function getPokemonInfo(id) {
    try {
        const { data } = await axios.get(`/pokemons/${id}`);
        return data;
    } catch (err) {
        console.log(err);
    }
}

export async function getRandomPokemonId() {
    try {
        const { data: pokemonId } = await axios.get('/pokemons/random');
        return pokemonId;
    } catch (err) {
        console.log(err);
    }
}