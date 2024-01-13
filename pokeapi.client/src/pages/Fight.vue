<script setup>
import { computed, onMounted, ref } from "vue";
import { useRoute } from "vue-router";

import FightCard from "@/components/FightCard.vue";
import {
  getPokemonInfo,
  getRandomPokemonId,
} from "../services/pokemonService.js";

const route = useRoute();
const myPoke = ref({});
const fightStat = ref([]);
const round = ref(1);
const enemyPoke = ref({});

const isFinished = computed(() => {
  return enemyPoke.value.hp === 0 || myPoke.value.hp === 0;
});

onMounted(async () => {
  const myPokemonId = route.query.myPokemonId;
  const enemyPokemonId = await getRandomPokemonId();
  myPoke.value = await preparePokemon(myPokemonId);
  enemyPoke.value = await preparePokemon(enemyPokemonId);
});

async function preparePokemon(pokemonId) {
  const pokemon = await getPokemonInfo(pokemonId);
  pokemon.maxHp = pokemon.hp;
  return pokemon;
}

function fightResult(winPokemon, losePokemon, isMyPokemonWin) {
  losePokemon.value = {
    ...losePokemon.value,
    hp: Math.max(losePokemon.value.hp - winPokemon.value.attackPower, 0),
  };

  fightStat.value.push({
    round: round.value,
    winPokemon: winPokemon.value.name,
    losePokemon: losePokemon.value.name,
    isMyPokemonWin: isMyPokemonWin,
  });
}

function onAttack() {
  const rnd = Math.floor(Math.random() * 2);
  if (rnd % 2 == 0) {
    fightResult(myPoke, enemyPoke, true);
  } else {
    fightResult(enemyPoke, myPoke, false);
  }
  round.value++;
}
</script>

<template>
  <div class="flex items-center justify-between">
    <FightCard
      :name="myPoke.name"
      :imageUrl="myPoke.imageUrl"
      :hp="myPoke.hp"
      :maxHp="myPoke.maxHp"
      :attackPower="myPoke.attackPower"
    ></FightCard>

    <div class="flex flex-col items-center">
      <table class="mb-10">
        <thead>
          <tr class="text-lg">
            <th class="px-4 border border-slate-300 rounded-md">Раунд</th>
            <th class="px-4 border border-slate-300 rounded-md">Победил</th>
            <th class="px-4 border border-slate-300 rounded-md">Проиграл</th>
          </tr>
        </thead>
        <tbody v-auto-animate>
          <tr
            v-for="stat in fightStat"
            :key="`stat_${stat.round}`"
            class="text-center"
            :class="stat.isMyPokemonWin ? 'bg-green-300' : 'bg-red-300'"
          >
            <td class="px-3 border border-slate-300">{{ stat.round }}</td>
            <td class="px-3 border border-slate-300">{{ stat.winPokemon }}</td>
            <td class="px-3 border border-slate-300">{{ stat.losePokemon }}</td>
          </tr>
        </tbody>
      </table>

      <button
        :disabled="isFinished"
        class="bg-red-500 text-white py-2 px-4 rounded-md disabled:bg-red-300"
        type="button"
        @click="onAttack"
      >
        Удар
      </button>
    </div>

    <FightCard
      :name="enemyPoke.name"
      :imageUrl="enemyPoke.imageUrl"
      :hp="enemyPoke.hp"
      :maxHp="enemyPoke.maxHp"
      :attackPower="enemyPoke.attackPower"
    ></FightCard>
  </div>
</template>