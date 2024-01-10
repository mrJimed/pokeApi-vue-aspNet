<script setup>
import { inject } from "vue";

import { logoutUser } from "../services/authService.js";

const { currentUser, changeUser } = inject("user");

async function logout() {
  await logoutUser();
  changeUser(null);
}
</script>

<template>
  <header class="py-3 border-b border-b-slate-200 shadow-sm rounded-full">
    <div class="flex justify-between items-center w-4/5 mx-auto">
      <router-link to="/">
        <img class="w-40 h-auto" src="/logo.png" alt="Pokemon" />
      </router-link>

      <ul v-if="!currentUser" class="flex items-center gap-4">
        <li>
          <router-link to="/authentication">
            <button
              class="border border-slate-200 py-1.5 px-3 rounded-md transition hover:bg-slate-100 hover:shadow-sm"
              type="button"
            >
              Войти
            </button>
          </router-link>
        </li>
      </ul>

      <ul v-else class="flex items-center gap-4">
        <li>
          <button
            class="border border-slate-200 py-1.5 px-3 rounded-md transition hover:bg-slate-100 hover:shadow-sm"
            type="button"
          >
            Закладки
          </button>
        </li>

        <li>
          <button
            class="border border-slate-200 py-1.5 px-3 rounded-md transition hover:bg-slate-100 hover:shadow-sm"
            type="button"
          >
            История
          </button>
        </li>

        <li>
          <button
            class="border border-slate-200 py-1.5 px-3 rounded-md transition hover:bg-slate-100 hover:shadow-sm"
            type="button"
            @click="logout"
          >
            Выход
          </button>
        </li>
      </ul>
    </div>
  </header>
</template>