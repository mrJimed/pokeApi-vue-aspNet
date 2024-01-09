<script setup>
defineProps({
  name: String,
  imageUrl: String,
  hp: Number,
  attackPower: Number,
});
</script>

<template>
  <div
    class="flex gap-x-6 bg-slate-50 items-center border border-slate-200 rounded-md px-8 py-5 shadow-sm transition hover:shadow-md hover:-translate-y-3"
  >
    <img class="w-24 h-24" :src="imageUrl" :alt="name" />
    <div class="select-none">
      <h1 class="text-lg font-bold">{{ name }}</h1>
      <h1 class="text-base">HP: {{ hp }}</h1>
      <h1 class="text-base">Атака: {{ attackPower }}</h1>
    </div>
  </div>
</template>