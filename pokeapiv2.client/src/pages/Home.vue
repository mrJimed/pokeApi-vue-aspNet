<script setup>
import Card from "@/components/Card.vue";
import Pagination from "@/components/Pagination.vue";

import { computed, onMounted, ref, watch } from "vue";
import { getAllPokemonsAsync } from "../services/pokemonService.js";

const allPokemons = ref([]);
const pagePokemons = ref([]);
const currentPage = ref(1);
const countPageItems = ref(12);

const pageCounts = computed(() =>
  Math.ceil(allPokemons.value.length / countPageItems.value)
);

onMounted(async () => {
  allPokemons.value = await getAllPokemonsAsync();
  pagePokemons.value = allPokemons.value.slice(0, countPageItems.value);
});

// watch on change currentPage variable
watch(currentPage, () => {
  const startIndex = (currentPage.value - 1) * countPageItems.value;
  const endIndex = currentPage.value * countPageItems.value;
  pagePokemons.value = allPokemons.value.slice(startIndex, endIndex);
});

// Pagination functions
function nextPageClick() {
  currentPage.value++;
}

function prevPageClick() {
  currentPage.value--;
}
</script>

<template>
  <div class="mb-5">
    <h2 class="text-2xl font-bold">
      Список покемонов ({{ allPokemons.length }})
    </h2>
  </div>

  <div class="grid grid-cols-4 gap-y-6 place-items-center mb-10">
    <Card
      :id="pokemon.id"
      v-for="pokemon in pagePokemons"
      :key="pokemon.id"
    ></Card>
  </div>

  <Pagination
    :pageCounts="pageCounts"
    :currentPage="currentPage"
    @nextPageClick="nextPageClick"
    @prevPageClick="prevPageClick"
  ></Pagination>
</template>