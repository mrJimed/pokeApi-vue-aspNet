<script setup>
import { getPokemonInfoAsync } from "@/services/pokemonService";
import { onMounted, ref } from "vue";
import { useRoute } from "vue-router";

const route = useRoute();
const pokemon = ref({});

onMounted(async () => {
  pokemon.value = await getPokemonInfoAsync(route.params.id);
});
</script>

<template>
  <div
    class="fixed flex flex-col top-1/2 left-1/2 -translate-y-1/2 px-8 py-6 border border-slate-400 bg-slate-100 rounded-md select-none"
  >
    <img
      class="w-32 h-auto bg-white border border-slate-300 rounded-md"
      :src="pokemon.imageUrl"
    />
    <div class="text-center">
      <h3 class="font-bold text-lg">{{ pokemon.name }}</h3>
      <p>Hp: {{ pokemon.hp }}</p>
      <p>Сила: {{ pokemon.attackPower }}</p>
    </div>
  </div>
</template>