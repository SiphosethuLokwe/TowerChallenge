<!-- src/App.vue or any other component -->
<template>
  <div>
    <PlayerStats :player="player" />
    <BetPanel @startGame="startGame" />
    <GameBoard :levels="1" :game="game" @selectBox="selectBox" />
  </div>
</template>

<script>
import { defineComponent } from 'vue';
import { usePlayerStore } from '../store/store'; // Updated import
import PlayerStats from './PlayerStats.vue';
import BetPanel from './BetPanel.vue';
import GameBoard from './GameBoard.vue';

export default defineComponent({
  components: {
    PlayerStats,
    BetPanel,
    GameBoard
  },
  setup() {
    const playerStore = usePlayerStore(); // Access Pinia store instance

    // Directly use store state and methods
    const player = playerStore.player;
    const game = playerStore.game;

    const startGame = (rows, difficulty, betAmount) => {
      console.log(rows, difficulty, betAmount); // Ensure parameters are correct heres
      playerStore.startGame({ rows, difficulty , betAmount });
    };

    const selectBox = (row, box) => {
      playerStore.selectBox({ gameId: game.id, row, box });
    };

    return {
      player,
      game,
      startGame,
      selectBox
    };
  },

  methods(){

  }
});
</script>
