<script setup>
defineProps({
  name: String,
  imageUrl: String,
  hp: Number,
  maxHp: Number,
  attackPower: Number,
});
</script>

<template>
  <div
    class="w-72 h-fit border border-slate-400 bg-slate-100 rounded-md px-10 py-4 shadow-sm"
  >
    <div class="flex gap-3 items-center">
      <img class="w-24 h-24" :src="imageUrl" :alt="name" />
      <div class="select-none">
        <h2 class="font-bold">{{ name }}</h2>
        <p class="text-base">Атака: {{ attackPower }}</p>
      </div>
    </div>

    <div class="w-full h-6 rounded-md overflow-hidden relative mt-2">
      <hr class="w-full bg-red-300 h-full" />
      <hr
        class="bg-red-500 h-full absolute top-0 left-0 z-10"
        :style="{ width: `${Math.round((hp * 100) / maxHp)}%` }"
      />
      <h4 class="absolute text-white top-0 left-1/2 -translate-x-1/2 z-20">
        {{ hp }} / {{ maxHp }}
      </h4>
    </div>
  </div>
</template>