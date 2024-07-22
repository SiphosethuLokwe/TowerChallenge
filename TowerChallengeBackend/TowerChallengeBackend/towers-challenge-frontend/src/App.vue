<!-- src/App.vue or any other component -->
<template>
  <div>
    <BetPanel @startGame="startGame" />
    <GameBoard v-if="game" :levels="game.levels" :game="game" @selectBox="selectBox" />
    <!-- <PlayerStats :player="player" /> -->

  </div>
</template>

<script>
import { defineComponent, onMounted , computed} from 'vue';
import { gamestore } from './store/gamestore'; 
import PlayerStats from './components/PlayerStats.vue';
import BetPanel from './components/BetPanel.vue';
import GameBoard from './components/GameBoard.vue';

export default defineComponent({
  components: {
    PlayerStats,
    BetPanel,
    GameBoard
  },
  setup() {
    const gameStore = gamestore(); 

    const player = computed(() => gameStore.playerDetails);
    const game = computed(() => gameStore.gameDetails);

    const startGame = (rows, difficulty, betAmount) => {
      gameStore.startGame(rows, difficulty, betAmount);
    };
     
    const selectBox = (gameid, row, box) => {
      gameStore.selectBox(gameid, row, box ) ;
    };
 
    return {
    
      player,
      game,
      startGame,
      selectBox
    };
  },
  


});
</script>
