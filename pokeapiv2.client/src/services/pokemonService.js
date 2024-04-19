import axios from "axios";

const ROOT_ROUTE = '/pokemons'

export async function getAllPokemonsAsync(sortIn, startWith) {
    const { data: pokemons } = await axios.get(`${ROOT_ROUTE}?sortIn=${sortIn}&startWith=${startWith}`);
    return pokemons;
}

export async function getPokemonInfoAsync(id) {
    const { data: pokemon } = await axios.get(`${ROOT_ROUTE}/${id}`);
    return pokemon;
}

export async function getRandomPokemonIdAsync() {
    const { data: id } = await axios.get(`${ROOT_ROUTE}/random-id`);
    return id;
}