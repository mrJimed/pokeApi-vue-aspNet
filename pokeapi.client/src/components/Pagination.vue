<script setup>
import { ref } from "vue";

const emits = defineEmits([
  "nextPageClick",
  "prevPageClick",
  "onChangeCurrentPage",
]);

const props = defineProps({
  maxPages: Number,
  currentPage: Number,
});

const isChangeCurrentPage = ref(false);

function unvisiblePageInfo() {
  isChangeCurrentPage.value = true;
}

function unvisibleInputPage(event) {
  isChangeCurrentPage.value = false;
  let newCurrentPage = Number(event.target.value);
  if (newCurrentPage < 1) {
    newCurrentPage = 1;
  } else if (newCurrentPage > props.maxPages) {
    newCurrentPage = props.maxPages;
  }
  emits("onChangeCurrentPage", newCurrentPage);
}
</script>

<template>
  <div class="flex gap-4 border w-fit rounded-md overflow-hidden select-none">
    <button
      @click="emits('prevPageClick')"
      :disabled="currentPage === 1"
      class="pl-2 pr-2 py-2 bg-slate-200 transition hover:bg-slate-300 active:bg-slate-400 disabled:bg-red-400 disabled:text-white"
      type="button"
    >
      Пред.
    </button>

    <div class="py-2">
      <input
        v-if="isChangeCurrentPage"
        class="w-28 text-center"
        type="number"
        autofocus
        :value="currentPage"
        :min="1"
        :max="maxPages"
        @keyup.enter="unvisibleInputPage"
      />
      <span v-else @click="unvisiblePageInfo"
        >{{ currentPage }} из {{ maxPages }}</span
      >
    </div>

    <button
      @click="emits('nextPageClick')"
      :disabled="currentPage === maxPages"
      class="pl-2 pr-2 py-2 bg-slate-200 transition hover:bg-slate-300 active:bg-slate-400 disabled:bg-red-400 disabled:text-white"
      type="button"
    >
      След.
    </button>
  </div>
</template>