<script setup>
import Card from "../components/Card.vue";
import Pagination from "../components/Pagination.vue";

import debounce from "lodash.debounce";
import { computed, onMounted, ref, watch } from "vue";
import { getAllPokemonsAsync } from "../services/pokemonService.js";

const allPokemons = ref([]);
const pagePokemons = ref([]);
const currentPage = ref(1);
const countPageItems = ref(12);
const sortIn = ref("default");
const searchPokemons = ref("");

const pageCounts = computed(() =>
  Math.ceil(allPokemons.value.length / countPageItems.value)
);

onMounted(async () => {
  await sortPokemonsAsync();
});

// watch on change sortIn variable
watch(sortIn, async () => {
  await sortPokemonsAsync();
  currentPage.value = 1;
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

// Sort functions
async function sortPokemonsAsync() {
  allPokemons.value = await getAllPokemonsAsync(
    sortIn.value,
    searchPokemons.value
  );
  pagePokemons.value = allPokemons.value.slice(0, countPageItems.value);
}

// Search functions
const onChangeInputAsync = debounce(async () => {
  await sortPokemonsAsync();
  currentPage.value = 1;
}, 500);

async function clearInputAsync() {
  searchPokemons.value = "";
  await sortPokemonsAsync();
  currentPage.value = 1;
}
</script>

<template>
  <div class="flex max-md:flex-col justify-between items-center mb-5">
    <h2 class="text-2xl font-bold max-md:mb-2">
      Список покемонов ({{ allPokemons.length }})
    </h2>

    <div class="flex flex-col gap-2">
      <div class="relative">
        <input
          class="border border-slate-200 rounded-md pl-5 pr-14 py-2 w-full outline-none placeholder:italic"
          type="text"
          placeholder="Поиск..."
          v-model="searchPokemons"
          @input="onChangeInputAsync"
        />

        <button
          class="absolute top-1/2 right-4 -translate-y-1/2"
          type="button"
          title="Очистить"
          @click="clearInputAsync"
        >
          <i class="fa-solid fa-eraser text-xl"></i>
        </button>
      </div>

      <div class="flex max-sm:flex-col-reverse max-sm:items-center justify-between items-start gap-4 select-none">
        <div class="flex border border-slate-200 rounded-md px-5 py-2">
          <label for="pageItemsSelect">Имя: </label>

          <select name="pageItemsSelect" class="outline-none" v-model="sortIn">
            <option value="asc">A-z</option>
            <option value="desc">Z-a</option>
            <option value="default">default</option>
          </select>
        </div>

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
  </div>

  <div
    class="grid max-sm:grid-cols-1 sm:grid-cols-1 md:grid-cols-2 lg:grid-cols-3 2xl:grid-cols-4 gap-y-6 place-items-center mb-10"
  >
    <Card
      :id="pokemon.id"
      v-for="pokemon in pagePokemons"
      :key="pokemon.id"
    ></Card>
  </div>

  <Pagination
  class="max-md:mx-auto"
    :pageCounts="pageCounts"
    :currentPage="currentPage"
    @onNextPageClick="onNextPageClick"
    @onPrevPageClick="onPrevPageClick"
    @onChangeCurrentPage="onChangeCurrentPage"
  ></Pagination>
</template>