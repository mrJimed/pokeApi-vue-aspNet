<script setup>
import { onMounted, ref } from "vue";

const emits = defineEmits(["onClose", "onSend"]);

defineProps({
  title: String,
  inputType: String,
  inputPlaceholder: String,
});

const inputText = ref("");
const inputTextElement = ref(null);

onMounted(() => {
  inputTextElement.value.focus();
});
</script>

<template>
  <div class="absolute top-0 left-0 w-full h-full z-50">
    <div class="bg-black w-full h-full opacity-70"></div>
    <div class="absolute top-0 left-0 w-full h-full">
      <div
        class="fixed border border-slate-400 bg-slate-100 w-fit top-1/4 left-1/2 -translate-x-1/2 py-10 px-14 rounded-md"
      >
        <h2
          class="text-center text-2xl font-bold border-b border-b-slate-400 pb-3"
        >
          {{ title }}
        </h2>
        <div class="flex flex-col mt-5">
          <input
            ref="inputTextElement"
            class="border border-slate-200 py-2 px-3 rounded-md outline-none transition focus:border-slate-300"
            :type="inputType"
            v-model="inputText"
            :placeholder="inputPlaceholder"
            on-focus
          />

          <div class="mt-4 flex justify-between">
            <button
              class="bg-green-500 py-1 px-3 rounded-md text-white hover:bg-green-600 active:bg-green-700"
              type="button"
              @click="emits('onSend', inputText)"
            >
              Отправить
            </button>
            <button
              class="bg-red-500 py-1 px-3 rounded-md text-white hover:bg-red-600 active:bg-red-700"
              type="button"
              @click="emits('onClose')"
            >
              Отмена
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>