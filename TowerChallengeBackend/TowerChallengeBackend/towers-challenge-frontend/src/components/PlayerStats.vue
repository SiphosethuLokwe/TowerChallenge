<template>
    <div>
      <p v-if="player">Balance: {{ player.balance }}</p>
    </div>
  </template>
  
  <script>
  import { defineComponent, computed } from 'vue';
  import { gamestore } from '../store/gamestore'; 
  
  export default defineComponent({
    setup() {
      const gamestore = gamestore();

      const player = computed(() => gamestore.playerDetails);

      return {
        player
      };
    }
  });
  </script>
  