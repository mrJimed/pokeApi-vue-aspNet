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
      class="pl-2 pr-2 py-2 bg-slate-200 transition hover:bg-slate-300 active:bg-slate-400 disabled:bg-red-400 disabled:text-white"
      type="button"
      @click="emits('prevPageClick')"
      :disabled="currentPage == 1"
    >
      Пред.
    </button>

    <span>{{ currentPage }} из {{ pageCounts }}</span>

    <button
      class="pl-2 pr-2 py-2 bg-slate-200 transition hover:bg-slate-300 active:bg-slate-400 disabled:bg-red-400 disabled:text-white"
      type="button"
      @click="emits('nextPageClick')"
      :disabled="currentPage == pageCounts"
    >
      След.
    </button>
  </div>
</template>