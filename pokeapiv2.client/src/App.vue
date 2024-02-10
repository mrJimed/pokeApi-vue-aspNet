<script setup>
import Header from "./components/Header.vue";
</script>

<template>
  <Header></Header>

  <div class="w-11/12 mx-auto mt-5">
    <router-view></router-view>
  </div>
</template>
