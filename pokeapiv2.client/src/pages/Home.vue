<script setup>
import Card from "../components/Card.vue";
import Pagination from "../components/Pagination.vue";

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

// watch on change countPageItems variable
watch(countPageItems, () => {
  currentPage.value = 1;
  pagePokemons.value = allPokemons.value.slice(0, countPageItems.value);
});

// Pagination functions
function onNextPageClick() {
  currentPage.value++;
}

function onPrevPageClick() {
  currentPage.value--;
}

function onChangeCurrentPage(newCurrentPage) {
  currentPage.value = newCurrentPage;
}
</script>

<template>
  <div class="flex justify-between items-center mb-5">
    <h2 class="text-2xl font-bold">
      Список покемонов ({{ allPokemons.length }})
    </h2>

    <div class="flex justify-between items-center select-none">
      <div class="flex border border-slate-200 rounded-md px-5 py-2">
        <label for="pageItemsSelect">Кол-во элементов: </label>

        <select
          name="pageItemsSelect"
          class="outline-none"
          v-model="countPageItems"
        >
          <option :value="12">12</option>
          <option :value="16">16</option>
          <option :value="20">20</option>
        </select>
      </div>
    </div>
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
    @onNextPageClick="onNextPageClick"
    @onPrevPageClick="onPrevPageClick"
    @onChangeCurrentPage="onChangeCurrentPage"
  ></Pagination>
</template>