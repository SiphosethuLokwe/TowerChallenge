<!-- src/App.vue or any other component -->
<template>
  <div>
    <BetPanel @startGame="startGame" />
    <GameBoard v-if="game" :levels="game.levels" :game="game" :boxResponse="gameresponse" @selectBox="selectBox" />
    <BoxResponseDisplay :response="gameresponse" />
    <PlayerStats v-if ="player" :player="player" /> 

  </div>
</template>

<script>
import { defineComponent, onMounted , computed} from 'vue';
import { gamestore } from './store/gamestore'; 
import PlayerStats from './components/PlayerStats.vue';
import BetPanel from './components/BetPanel.vue';
import GameBoard from './components/GameBoard.vue';
import BoxResponseDisplay from './components/GameResponse.vue'
export default defineComponent({
  components: {
    PlayerStats,
    BetPanel,
    GameBoard,
    BoxResponseDisplay
  },
  setup() {
    const gameStore = gamestore(); 

    const player = computed(() => gameStore.playerDetails);
    const game = computed(() => gameStore.gameDetails);
    const gameresponse = computed (() => gameStore.boxGamePlayDetails);

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
      selectBox,
      gameresponse
    };
  },
  


});
</script>
