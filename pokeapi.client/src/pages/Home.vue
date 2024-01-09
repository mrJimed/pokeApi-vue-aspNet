<script type="module" setup>
import debounce from "lodash.debounce";
import { computed, onMounted, ref, watch } from "vue";
import { getPokemons, getPokemonInfo } from "../services/pokemonService.js";

import CardList from "../components/CardList.vue";
import Pagination from "../components/Pagination.vue";

const pokemons = ref([]);
const pagePokemons = ref([]);
const currentPage = ref(1);
const countPageItems = ref(12);

const maxPages = computed(() =>
  Math.ceil(pokemons.value.length / countPageItems.value)
);

onMounted(async () => {
  pokemons.value = await getPokemons();
  pagePokemons.value = pokemons.value.slice(0, countPageItems.value);
});

watch(pokemons, () => {
  pagePokemons.value = pokemons.value.slice(0, countPageItems.value);
});

watch(currentPage, () => {
  const startIndex = (currentPage.value - 1) * countPageItems.value;
  const endIndex = currentPage.value * countPageItems.value;
  pagePokemons.value = pokemons.value.slice(startIndex, endIndex);
});

watch(countPageItems, () => {
  currentPage.value = 1;
  pagePokemons.value = pokemons.value.slice(0, countPageItems.value);
});

watch(pagePokemons, async () => {
  for (let i = 0; i < pagePokemons.value.length; i++) {
    pagePokemons.value[i] = await getPokemonInfo(pagePokemons.value[i].id);
  }
});

const onChangeInput = debounce(async (event) => {
  const startWith = event.target.value;
  pokemons.value = await getPokemons(startWith);
  currentPage.value = 1;
}, 500);

function onChangeCurrentPage(newCurrentPage) {
  currentPage.value = newCurrentPage;
}

function onChangeCountPageItems(event) {
  let newCountPageItems = Number(event.target.value);
  countPageItems.value = newCountPageItems;
}

function nextPageClick() {
  currentPage.value++;
}

function prevPageClick() {
  currentPage.value--;
}
</script>

<template>
  <div class="flex justify-between items-center">
    <h2 class="text-2xl font-bold">Список покемонов ({{ pokemons.length }})</h2>

    <div class="flex gap-3">
      <div class="flex justify-between items-center select-none">
        <div class="flex border border-slate-200 rounded-md px-5 py-2">
          <label for="pageItemsSelect">Кол-во элементов: </label>

          <select name="pageItemsSelect" class="outline-none" @change="onChangeCountPageItems">
            <option>12</option>
            <option>16</option>
            <option>20</option>
          </select>
        </div>
      </div>

      <input
        class="border border-slate-200 px-10 py-1.5 rounded-md outline-none transition focus:border-slate-300"
        type="text"
        placeholder="Поиск..."
        @input="onChangeInput"
      />
    </div>
  </div>
  <div class="my-10">
    <CardList :items="pagePokemons"></CardList>

    <div class="mt-5">
      <Pagination
        @nextPageClick="nextPageClick"
        @prevPageClick="prevPageClick"
        @onChangeCurrentPage="onChangeCurrentPage"
        :currentPage="currentPage"
        :maxPages="maxPages"
      ></Pagination>
    </div>
  </div>
</template>