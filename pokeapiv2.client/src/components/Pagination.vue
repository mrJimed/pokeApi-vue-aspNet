<script setup>
defineProps({
  pageCounts: Number,
  currentPage: Number,
});

const emits = defineEmits(["nextPageClick", "prevPageClick"]);
</script>

<template>
  <div
    class="flex gap-4 border border-slate-400 w-fit rounded-md overflow-hidden select-none items-center"
  >
    <button
      class="px-3 py-2 bg-slate-200 transition hover:bg-slate-300 active:bg-slate-400 disabled:bg-red-400 disabled:text-white"
      type="button"
      @click="emits('prevPageClick')"
      :disabled="currentPage == 1"
    >
      <i class="fa-solid fa-chevron-left"></i>
    </button>

    <span>{{ currentPage }} из {{ pageCounts }}</span>

    <button
      class="px-3 py-2 bg-slate-200 transition hover:bg-slate-300 active:bg-slate-400 disabled:bg-red-400 disabled:text-white"
      type="button"
      @click="emits('nextPageClick')"
      :disabled="currentPage == pageCounts"
    >
      <i class="fa-solid fa-chevron-right"></i>
    </button>
  </div>
</template>