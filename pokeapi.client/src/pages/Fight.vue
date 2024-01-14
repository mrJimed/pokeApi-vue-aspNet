<script setup>
import { computed, onMounted, ref } from "vue";
import { useRoute } from "vue-router";

import InputModal from "../components/InputModal.vue";
import FightCard from "../components/FightCard.vue";
import FightStatistic from "../components/FightStatistic.vue";

import { sendMessage } from "../services/mailService.js";
import {
  getPokemonInfo,
  getRandomPokemonId,
} from "../services/pokemonService.js";

const route = useRoute();
const myPoke = ref({});
const fightStat = ref([]);
const round = ref(1);
const enemyPoke = ref({});
const sendStatisticFormVisible = ref(false);

const isFinished = computed(() => {
  return enemyPoke.value.hp === 0 || myPoke.value.hp === 0;
});

onMounted(async () => {
  const myPokemonId = route.query.myPokemonId;
  const enemyPokemonId = await getRandomPokemonId();
  myPoke.value = await preparePokemon(myPokemonId);
  enemyPoke.value = await preparePokemon(enemyPokemonId);
});

function onCloseSendStatisticForm() {
  sendStatisticFormVisible.value = false;
}

async function onSendStatisticForm(toEmail) {
  const result = fightStat.value.at(-1);
  await sendMessage(
    toEmail,
    `Победил: ${result.winPokemon.name}\n` + `Кол-во раундов: ${result.round}`,
    `Бой: ${myPoke.value.name} vs ${enemyPoke.value.name}`
  );
  sendStatisticFormVisible.value = false;
}

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
    winPokemon: winPokemon.value,
    losePokemon: losePokemon.value,
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
      <FightStatistic class="mb-10" :fightStat="fightStat"></FightStatistic>
      <button
        v-if="!isFinished"
        class="bg-red-500 text-white py-2 px-4 rounded-md"
        type="button"
        @click="onAttack"
      >
        Удар
      </button>

      <button
        v-else
        class="bg-blue-400 text-white py-2 px-4 rounded-md"
        type="button"
        @click="sendStatisticFormVisible = true"
      >
        Отправить результат
      </button>
    </div>

    <FightCard
      :name="enemyPoke.name"
      :imageUrl="enemyPoke.imageUrl"
      :hp="enemyPoke.hp"
      :maxHp="enemyPoke.maxHp"
      :attackPower="enemyPoke.attackPower"
    ></FightCard>

    <InputModal
      v-if="sendStatisticFormVisible"
      title="Окно отправки статистики"
      inputPlaceholder="Введите email"
      inputType="email"
      @onClose="onCloseSendStatisticForm"
      @onSend="onSendStatisticForm"
    ></InputModal>
  </div>
</template>