import axios from "axios";

const ROOT_ROUTE = '/pokemons'

export async function getAllPokemonsAsync() {
    const { data: pokemons } = await axios.get(ROOT_ROUTE);
    return pokemons;
}

export async function getPokemonInfoAsync(id) {
    const { data: pokemon } = await axios.get(`${ROOT_ROUTE}/${id}`);
    return pokemon;
}