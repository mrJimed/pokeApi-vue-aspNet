<script setup>
import { onMounted, ref } from "vue";
import { getPokemonInfoAsync } from "../services/pokemonService.js";

const props = defineProps({
  id: Number,
});

const pokemon = ref({});

onMounted(async () => {
  pokemon.value = await getPokemonInfoAsync(props.id);
});
</script>

<template>
  <div
    class="flex gap-1 flex-col w-72 px-4 py-2 border border-slate-400 bg-slate-100 rounded-md select-none"
  >
    <div class="flex items-center gap-6">
      <img
        class="w-32 h-auto bg-white border border-slate-300 rounded-md"
        :src="pokemon.imageUrl"
      />
      <div>
        <h3 class="font-bold text-lg">{{ pokemon.name }}</h3>
        <p>Hp: {{ pokemon.hp }}</p>
        <p>Сила: {{ pokemon.attackPower }}</p>
      </div>
    </div>
    <a :href="`/pokemon/${id}`" class="bg-green-500 text-center text-white py-1 rounded-md transition hover:bg-green-600">Открыть</a>
  </div>
  <!-- <div
    class="flex w-72 px-4 py-2 items-center gap-6 border border-slate-400 bg-slate-100 rounded-md select-none"
  >
    <img
      class="w-32 h-auto bg-white border border-slate-300 rounded-md"
      :src="pokemon.imageUrl"
    />
    <div>
      <h3 class="font-bold text-lg">{{ pokemon.name }}</h3>
      <p>Hp: {{ pokemon.hp }}</p>
      <p>Сила: {{ pokemon.attackPower }}</p>
    </div>
  </div> -->
</template>