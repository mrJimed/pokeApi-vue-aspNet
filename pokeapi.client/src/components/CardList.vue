<script setup>
import Card from "./Card.vue";

defineProps({
  items: Array,
});
</script>

<template>
  <div class="grid grid-cols-4 gap-x-10 gap-y-6" v-auto-animate>
    <Card
      v-for="item in items"
      v-bind:key="item.id"
      :itemId="item.id"
      :name="item.name"
      :imageUrl="item.imageUrl"
      :hp="item.hp"
      :attackPower="item.attackPower"
    ></Card>
  </div>
</template>