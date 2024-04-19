<script setup>
defineProps({
  fightStat: Array,
});
</script>

<template>
  <table>
    <thead>
      <tr class="text-lg">
        <th class="px-4 border border-slate-300 rounded-md">Раунд</th>
        <th class="px-4 border border-slate-300 rounded-md">Победил</th>
        <th class="px-4 border border-slate-300 rounded-md">Проиграл</th>
      </tr>
    </thead>
    <tbody v-auto-animate>
      <tr
        v-for="stat in fightStat"
        :key="`stat_${stat.round}`"
        class="text-center"
        :class="stat.isMyPokemonWin ? 'bg-green-300' : 'bg-red-300'"
      >
        <td class="px-3 border border-slate-300">{{ stat.round }}</td>
        <td class="px-3 border border-slate-300">
          {{ stat.winPokemon.name }}
        </td>
        <td class="px-3 border border-slate-300">
          {{ stat.losePokemon.name }}
        </td>
      </tr>
    </tbody>
  </table>
</template>