<script setup>
import { inject, ref } from "vue";
import InputModal from "../components/InputModal.vue";

import { sendMessage } from "../services/mailService.js";
import { randomInteger } from "../services/helperService.js";
import {
  registrationUser,
  loginUser,
  resetUserPassword,
} from "../services/authService.js";

// auth/reg values
const isRegistration = ref(false);
const errMessage = ref(null);
const username = ref("");
const email = ref("");
const password = ref("");

// Reset password values
let code = "";
const isCodeValid = ref(false);
const isResetPasswordForm = ref(false);

const { changeUser } = inject("user");

function onChangeRegistrationStatus() {
  isRegistration.value = !isRegistration.value;
  errMessage.value = null;
}

async function resetPassword() {
  isResetPasswordForm.value = true;
  code = `${randomInteger(1000, 10_000)}`;
  await sendMessage(
    email.value,
    `Код для восстановляения пароля к сервису PokeApi: ${code}`,
    "Сброс пароля PokeApi"
  );
}

function checkCode(inputCode) {
  isResetPasswordForm.value = false;
  if (inputCode === code) {
    isCodeValid.value = true;
  }
}

async function sendNewPassword(newPassword) {
  await resetUserPassword(email.value, newPassword);
  isCodeValid.value = false;
}

async function submitRegistration(event) {
  event.preventDefault();

  const user = await registrationUser(
    username.value,
    email.value,
    password.value
  );
  if (user === "") {
    errMessage.value = "Пользователь с таким email уже существует";
  } else {
    changeUser(user);
    errMessage.value = null;
  }
}

async function submitLogin(event) {
  event.preventDefault();

  const user = await loginUser(email.value, password.value);
  if (user === "") {
    errMessage.value = "Неправильный email или пароль";
  } else {
    changeUser(user);
    errMessage.value = null;
  }
}
</script>

<template>
  <div>
    <div
      class="fixed w-1/3 left-1/2 top-1/4 -translate-x-1/2 border border-slate-200 rounded-md shadow-sm overflow-hidden"
    >
      <h1
        class="text-2xl text-center font-bold border-b border-b-slate-200 py-5"
      >
        {{ isRegistration ? "Регистрация профиля" : "Вход в профиль" }}
      </h1>

      <form class="flex flex-col py-5 px-10" v-auto-animate>
        <h3
          v-if="errMessage"
          class="mb-2 rounded-md px-2 py-1 bg-red-500 text-white"
        >
          {{ errMessage }}
        </h3>

        <input
          v-if="isRegistration"
          class="border border-slate-200 py-2 px-4 transition rounded-md outline-none focus:border-slate-300 focus:shadow-sm mb-2"
          type="text"
          v-model="username"
          :required="isRegistration ? true : false"
          placeholder="Введите имя пользователя..."
        />

        <input
          class="border border-slate-200 py-2 px-4 transition rounded-md outline-none focus:border-slate-300 focus:shadow-sm mb-2"
          type="email"
          v-model="email"
          required
          placeholder="Введите email..."
        />

        <input
          class="border border-slate-200 py-2 px-4 transition rounded-md outline-none focus:border-slate-300 focus:shadow-sm"
          type="password"
          v-model="password"
          required
          placeholder="Введите пароль..."
        />

        <div class="flex justify-between items-center mt-4">
          <div class="flex flex-col items-start gap-2">
            <button
              class="hover:underline"
              type="button"
              @click="onChangeRegistrationStatus"
            >
              {{ isRegistration ? "Есть аккаунт?" : "Нет аккаунта?" }}
            </button>

            <button
              v-if="!isRegistration"
              class="hover:underline"
              type="button"
              @click="resetPassword"
            >
              Забыли пароль?
            </button>
          </div>

          <input
            class="border cursor-pointer border-slate-200 px-4 py-2 rounded-md transition hover:bg-slate-100 hover:shadow-sm active:bg-slate-200"
            type="submit"
            @click="
              isRegistration ? submitRegistration($event) : submitLogin($event)
            "
            :value="isRegistration ? 'Регистрация' : 'Вход'"
          />
        </div>
      </form>
    </div>

    <InputModal
      v-if="isResetPasswordForm"
      title="Окно сброса пароля"
      inputPlaceholder="Введите код"
      inputType="text"
      @onClose="isResetPasswordForm = false"
      @onSend="checkCode"
    ></InputModal>

    <InputModal
      v-if="isCodeValid"
      title="Окно восстановления пароля"
      inputPlaceholder="Введите новый пароль"
      inputType="text"
      @onClose="isCodeValid = false"
      @onSend="sendNewPassword"
    ></InputModal>
  </div>
</template>