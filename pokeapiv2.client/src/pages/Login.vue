<script setup>
import { ref } from "vue";
import {
  generateResetPasswordCodeAsync,
  loginUserAsync,
  resetPasswordAsync,
} from "../services/userService.js";
import { useRouter } from "vue-router";
import { useStore } from "vuex";
import InputModal from "../components/InputModal.vue";

const router = useRouter();
const store = useStore();

const email = ref("");
const password = ref("");
const errorMessage = ref("");

// reset password
let resetCode = null;
const isCheckResetPasswordCodeForm = ref(false);
const isInputNewPasswordForm = ref(false);

async function onLoginSubmitAsync() {
  try {
    const user = await loginUserAsync(email.value, password.value);
    store.dispatch("login", user.username);
    router.push({ name: "Home" });
  } catch (ex) {
    const {
      response: { data },
    } = ex;
    errorMessage.value = data;
  }
}

async function onGenerateResetPasswordCode() {
  try {
    resetCode = await generateResetPasswordCodeAsync(email.value);
    isCheckResetPasswordCodeForm.value = true;
  } catch (ex) {
    const {
      response: { data },
    } = ex;
    errorMessage.value = data;
  }
}

function onCheckCorrectResetCode(inputCode) {
  isCheckResetPasswordCodeForm.value = false;
  if (Number(inputCode) == resetCode) {
    isCheckResetPasswordCodeForm.value = false;
    isInputNewPasswordForm.value = true;
    resetCode = null;
  }
}

async function onSendNewPasswordAsync(newPassword) {
  await resetPasswordAsync(email.value, newPassword);
  isInputNewPasswordForm.value = false;
}
</script>

<template>
  <div>
    <form
      class="w-1/3 max-lg:w-1/2 max-sm:w-10/12 bg-slate-100 px-4 pt-10 pb-7 border border-slate-400 rounded-md fixed top-1/4 left-1/2 -translate-x-1/2"
      @submit.prevent="onLoginSubmitAsync"
    >
      <h2
        class="font-bold text-center text-2xl border-b border-b-slate-300 pb-3"
      >
        Форма авторизации
      </h2>

      <div class="flex flex-col gap-3 mt-4">
        <p
          v-if="errorMessage"
          class="text-center font-bold bg-red-600 text-white py-2 rounded-md select-none cursor-pointer transition hover:bg-red-700"
          @click="() => (errorMessage = '')"
        >
          {{ errorMessage }}
        </p>
        <input
          required
          class="border border-slate-300 py-2 px-3 outline-none rounded-md transition focus:border-slate-400 placeholder:italic"
          type="email"
          placeholder="Введите email..."
          v-model="email"
        />
        <input
          required
          minlength="4"
          class="border border-slate-300 py-2 px-3 outline-none rounded-md transition focus:border-slate-400 placeholder:italic"
          type="password"
          placeholder="Введите пароль..."
          v-model="password"
        />
      </div>

      <div class="flex items-center justify-between mt-5">
        <div class="flex flex-col gap-y-1">
          <router-link to="/registration" class="hover:underline"
            >Нет аккаунта?</router-link
          >

          <button class="hover:underline" @click="onGenerateResetPasswordCode">
            Забыли пароль?
          </button>
        </div>

        <input
          class="bg-green-500 text-white rounded-md cursor-pointer hover:bg-green-600 active:bg-green-700 py-2 px-3"
          type="submit"
          value="Войти"
        />
      </div>
    </form>

    <InputModal
      v-if="isCheckResetPasswordCodeForm"
      title="Окно сброса пароля"
      inputPlaceholder="Введите код"
      inputType="text"
      @onClose="isCheckResetPasswordCodeForm = false"
      @onSend="onCheckCorrectResetCode"
    ></InputModal>

    <InputModal
      v-if="isInputNewPasswordForm"
      title="Окно восстановления пароля"
      inputPlaceholder="Введите новый пароль"
      inputType="text"
      @onClose="isInputNewPasswordForm = false"
      @onSend="onSendNewPasswordAsync"
    ></InputModal>
  </div>
</template>