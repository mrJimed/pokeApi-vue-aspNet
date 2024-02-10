<script setup>
import Header from "./components/Header.vue";
</script>

<template>
  <Header></Header>

  <div class="w-11/12 mx-auto mt-5 mb-7">
    <router-view></router-view>
  </div>
</template>
