<script setup>
defineProps({
  itemId: Number,
  name: String,
  imageUrl: String,
  hp: Number,
  attackPower: Number,
});
</script>

<template>
  <div
    class="bg-slate-50 border border-slate-200 rounded-md px-8 py-4 shadow-sm transition hover:shadow-md hover:-translate-y-3"
  >
    <div class="flex gap-x-6 items-center">
      <img class="w-24 h-24" :src="imageUrl" :alt="name" />
      <div class="select-none">
        <h1 class="text-lg font-bold">{{ name }}</h1>
        <p class="text-base">HP: {{ hp }}</p>
        <p class="text-base">Атака: {{ attackPower }}</p>
      </div>
    </div>
    <div class="mt-3">
      <router-link :to="`fight?myPokemonId=${itemId}`">
        <button
          class="bg-black text-white w-full rounded-md text-center py-0.5"
          type="button"
        >
          В бой
        </button>
      </router-link>
    </div>
  </div>
</template>