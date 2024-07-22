<template>
    <div>
      <p v-if="player">Balance: {{ player.balance }}</p>
    </div>
  </template>
  
  <script>
  import { defineComponent } from 'vue';
  import { usePlayerStore } from '../store/store';
  
  export default defineComponent({
    setup() {
      const playerStore = usePlayerStore();
      const player = playerStore.player;
      return {
        player
      };
    }
  });
  </script>
  