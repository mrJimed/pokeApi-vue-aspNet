<script setup>
import Card from "@/components/Card.vue";

import { onMounted, ref } from "vue";
import { getAllPokemonsAsync } from "../services/pokemonService.js";

const pokemons = ref([]);

onMounted(async () => {
  pokemons.value = await getAllPokemonsAsync();
});
</script>

<template>
  <div class="mb-5">
    <h2 class="text-2xl font-bold">Список покемонов ({{ pokemons.length }})</h2>
  </div>

  <div class="grid grid-cols-4 gap-y-6 place-items-center">
    <Card
      :id="pokemon.id"
      v-for="pokemon in pokemons.slice(0, 12)"
      :key="pokemon.id"
    ></Card>
  </div>
</template>