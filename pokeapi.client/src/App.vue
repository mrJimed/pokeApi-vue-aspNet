<script setup>
import { onMounted, provide, ref } from "vue";
import Header from "./components/Header.vue";

import { getCurrentUser } from "./services/authService.js";

const currentUser = ref(null);

onMounted(async () => {
  currentUser.value = await getCurrentUser();
});

provide("user", {
  currentUser,
  changeUser,
});

function changeUser(updatedUser) {
  currentUser.value = updatedUser;
}
</script>

<template>
  <Header></Header>
  <div class="w-4/5 mx-auto">
    <div class="mt-10">
      <router-view></router-view>
    </div>
  </div>
</template>