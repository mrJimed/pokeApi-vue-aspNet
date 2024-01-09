<template>
  <header class="py-3 border-b border-b-slate-200 shadow-sm rounded-full">
    <div class="flex justify-between items-center w-4/5 mx-auto">
      <img class="w-40 h-auto" src="/logo.png" alt="Pokemon" />

      <ul class="flex items-center gap-4">
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
      </ul>
    </div>
  </header>
</template>