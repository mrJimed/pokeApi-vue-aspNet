<script setup>
import { ref } from "vue";

const props = defineProps({
  pageCounts: Number,
  currentPage: Number,
});

const emits = defineEmits([
  "onNextPageClick",
  "onPrevPageClick",
  "onChangeCurrentPage",
]);

const isChangeCurrentPage = ref(false);

function toPage(newCurrentPage) {
  if (newCurrentPage < 1) {
    newCurrentPage = 1;
  } else if (newCurrentPage > props.pageCounts) {
    newCurrentPage = props.pageCounts;
  }
  emits("onChangeCurrentPage", newCurrentPage);
}

// functions on change isChangeCurrentPage variable
function unvisiblePageInfo() {
  isChangeCurrentPage.value = true;
}

function unvisibleInputPage(event) {
  isChangeCurrentPage.value = false;
  let newCurrentPage = Number(event.target.value);
  toPage(newCurrentPage);
}
</script>

<template>
  <div
    class="flex gap-4 border border-slate-400 w-fit rounded-md overflow-hidden select-none items-center"
  >
    <div class="flex">
      <button
        class="px-3 py-2 bg-slate-200 transition hover:bg-slate-300 active:bg-slate-400 disabled:bg-red-400 disabled:text-white"
        type="button"
        :disabled="currentPage == 1"
        @click="toPage(1)"
      >
        <i class="fa-solid fa-angles-left"></i>
      </button>

      <button
        class="px-3 py-2 bg-slate-200 transition hover:bg-slate-300 active:bg-slate-400 disabled:bg-red-400 disabled:text-white"
        type="button"
        @click="emits('onPrevPageClick')"
        :disabled="currentPage == 1"
      >
        <i class="fa-solid fa-chevron-left"></i>
      </button>
    </div>

    <div>
      <input
        v-if="isChangeCurrentPage == true"
        :value="currentPage"
        :min="1"
        :max="maxPages"
        autofocus
        class="w-28 text-center"
        type="number"
        @keyup.enter="unvisibleInputPage"
      />
      <span v-else @click="unvisiblePageInfo" class="cursor-pointer"
        >{{ currentPage }} из {{ pageCounts }}</span
      >
    </div>

    <div class="flex">
      <button
        class="px-3 py-2 bg-slate-200 transition hover:bg-slate-300 active:bg-slate-400 disabled:bg-red-400 disabled:text-white"
        type="button"
        @click="emits('onNextPageClick')"
        :disabled="currentPage == pageCounts"
      >
        <i class="fa-solid fa-chevron-right"></i>
      </button>

      <button
        class="px-3 py-2 bg-slate-200 transition hover:bg-slate-300 active:bg-slate-400 disabled:bg-red-400 disabled:text-white"
        type="button"
        :disabled="currentPage == pageCounts"
        @click="toPage(pageCounts)"
      >
        <i class="fa-solid fa-angles-right"></i>
      </button>
    </div>
  </div>
</template>