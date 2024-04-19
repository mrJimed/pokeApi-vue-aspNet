<script setup>
import { onMounted, ref } from "vue";
import { useRouter } from "vue-router";
import {
  getPokemonInfoAsync,
  getRandomPokemonIdAsync,
} from "../services/pokemonService.js";

const router = useRouter();
const props = defineProps({
  id: Number,
});

const pokemon = ref({});

onMounted(async () => {
  pokemon.value = await getPokemonInfoAsync(props.id);
});

// fight functions
async function goToFightPage(myPokemonId) {
  const enemyPokemonId = await getRandomPokemonIdAsync();
  router.push({
    name: "Fight",
    query: {
      myPokemon: myPokemonId,
      enemyPokemon: enemyPokemonId,
    },
  });
}
</script>

<template>
  <div
    class="flex gap-1.5 flex-col w-72 px-4 py-2 border border-slate-400 bg-slate-100 rounded-md select-none"
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

        <div class="flex items-center gap-x-1.5 mt-1.5">
          <a
            title="Открыть"
            :href="`/pokemon/${id}`"
            class="bg-green-500 px-2 text-white py-1 rounded-md transition hover:bg-green-600"
          >
            <i class="fa-solid fa-magnifying-glass"></i>
          </a>

          <button
            @click="goToFightPage(id)"
            title="В бой"
            type="button"
            class="bg-red-500 px-2 text-white py-1 rounded-md transition hover:bg-red-600"
          >
            <i class="fa-solid fa-baseball-bat-ball"></i>
          </button>
        </div>
      </div>
    </div>
  </div>
</template>