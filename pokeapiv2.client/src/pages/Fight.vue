<script setup>
import { getPokemonInfoAsync } from "@/services/pokemonService";
import { computed, onMounted, ref } from "vue";
import { useRoute } from "vue-router";
import FightCard from "../components/FightCard.vue";
import FightStatistic from "../components/FightStatistic.vue";

const route = useRoute();

// fighters
const myPokemon = ref({});
const enemyPokemon = ref({});

// fight stat
let round = 1;
const fightStat = ref([]);
const isFinished = computed(() => {
  return enemyPokemon.value.hp === 0 || myPokemon.value.hp === 0;
});

onMounted(async () => {
  const myPokemonId = route.query.myPokemon;
  const enemyPokemonId = route.query.enemyPokemon;

  myPokemon.value = await preparePokemon(myPokemonId);
  enemyPokemon.value = await preparePokemon(enemyPokemonId);
});

async function preparePokemon(pokemonId) {
  const pokemon = await getPokemonInfoAsync(pokemonId);
  pokemon.maxHp = pokemon.hp;
  return pokemon;
}

// fight functions
function fightResult(winPokemon, losePokemon, isMyPokemonWin) {
  losePokemon.value = {
    ...losePokemon.value,
    hp: Math.max(losePokemon.value.hp - winPokemon.value.attackPower, 0),
  };

  fightStat.value.push({
    round: round,
    winPokemon: winPokemon.value,
    losePokemon: losePokemon.value,
    isMyPokemonWin: isMyPokemonWin,
  });
}

function onAttack() {
  const rnd = Math.floor(Math.random() * 2);
  if (rnd % 2 == 0) {
    fightResult(myPokemon, enemyPokemon, true);
  } else {
    fightResult(enemyPokemon, myPokemon, false);
  }
  round++;
}
</script>

<template>
  <div class="flex items-start justify-between">
    <FightCard
      :imageUrl="myPokemon.imageUrl"
      :name="myPokemon.name"
      :hp="myPokemon.hp"
      :maxHp="myPokemon.maxHp"
      :attackPower="myPokemon.attackPower"
    >
    </FightCard>
    <div class="flex flex-col items-center">
      <FightStatistic class="mb-10" :fightStat="fightStat"></FightStatistic>

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
      :imageUrl="enemyPokemon.imageUrl"
      :name="enemyPokemon.name"
      :hp="enemyPokemon.hp"
      :maxHp="enemyPokemon.maxHp"
      :attackPower="enemyPokemon.attackPower"
    >
    </FightCard>
  </div>
</template>