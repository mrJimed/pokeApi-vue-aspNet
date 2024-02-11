<template>
  <header class="bg-slate-100 shadow-md py-3">
    <div class="w-11/12 mx-auto flex items-center justify-between">
        <router-link to="/">
          <img class="w-40 h-auto" src="/logo.png" alt="Pokemon" />
        </router-link>

      <router-link to="/login" title="Вход">
        <i class="fa-solid fa-right-to-bracket text-xl"></i>
      </router-link>
    </div>
  </header>
</template>