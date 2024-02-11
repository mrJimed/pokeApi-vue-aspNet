<script setup>
import { ref } from "vue";
import { useRouter } from "vue-router";
import { useStore } from "vuex";
import { registrationUserAsync } from "../services/userService.js";

const router = useRouter();
const store = useStore();

const username = ref("");
const email = ref("");
const password = ref("");

async function onRegistrationSubmitAsync() {
  try {
    const user = await registrationUserAsync(
      username.value,
      email.value,
      password.value
    );
    store.dispatch("login", user.username);
    router.push({ name: "Home" });
  } catch (ex) {
    console.log(ex);
  }
}
</script>

<template>
  <form
    class="w-1/3 max-lg:w-1/2 max-sm:w-10/12 bg-slate-100 px-4 pt-10 pb-7 border border-slate-400 rounded-md fixed top-1/4 left-1/2 -translate-x-1/2"
    @submit.prevent="onRegistrationSubmitAsync"
  >
    <h2 class="font-bold text-center text-2xl border-b border-b-slate-300 pb-3">
      Форма регистрации
    </h2>

    <div class="flex flex-col gap-3 mt-4">
      <input
        required
        class="border border-slate-300 py-2 px-3 outline-none rounded-md transition focus:border-slate-400 placeholder:italic"
        type="text"
        minlength="4"
        placeholder="Введите логин..."
        v-model="username"
      />
      <input
        required
        class="border border-slate-300 py-2 px-3 outline-none rounded-md transition focus:border-slate-400 placeholder:italic"
        type="email"
        placeholder="Введите email..."
        v-model="email"
      />
      <input
        required
        class="border border-slate-300 py-2 px-3 outline-none rounded-md transition focus:border-slate-400 placeholder:italic"
        type="password"
        minlength="4"
        placeholder="Введите пароль..."
        v-model="password"
      />
    </div>

    <input
      class="bg-green-500 text-white rounded-md cursor-pointer hover:bg-green-600 active:bg-green-700 py-2 px-3 mt-5"
      type="submit"
      value="Создать аккаунт"
    />
  </form>
</template>