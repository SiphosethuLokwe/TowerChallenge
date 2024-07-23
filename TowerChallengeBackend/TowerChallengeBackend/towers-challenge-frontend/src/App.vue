<template>
  <div>
    <BetPanel @startGame="startGame" @continueGame="continueGame" @cashOut="cashOut" :gameResponse="gameresponse" />
    <GameBoard v-if="game" :levels="game.levels" :game="game" :boxResponse="gameresponse" @selectBox="selectBox" :cashout="cashOut" />
    <BoxResponseDisplay :response="gameresponse" />
  </div>
</template>

<script>
import { defineComponent, computed } from 'vue';
import { gamestore } from './store/gamestore'; 
import PlayerStats from './components/PlayerStats.vue';
import BetPanel from './components/BetPanel.vue';
import GameBoard from './components/GameBoard.vue';
import BoxResponseDisplay from './components/GameResponse.vue';
import {ElMessage} from 'element-plus'

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
    const gameresponse = computed(() => gameStore.boxGamePlayDetails);

    const startGame = (rows, difficulty, betAmount) => {
      if (rows < 4 || rows > 8){
        ElMessage.error("Rows must be bwtween 4 and 8 ");
        return;
      }
      if (betAmount <= 0){
         ElMessage.error("Please enter amount");
         return;
      }
      if (difficulty == ''){
         ElMessage.error("Please enter difficulty");
         return;
      }
      gameStore.startGame(rows, difficulty, betAmount);
    };

    const continueGame = () => {
      // Logic for continuing the game can be added here if needed
      console.log('Continue game logic');
    };
    const cashOut = (iscashout) => {
    const cashoutDetails = gameresponse.value;
    console.log(cashoutDetails, iscashout);
    return {cashoutDetails, iscashout}
    };

    const selectBox = (gameid, row, box) => {
      gameStore.selectBox(gameid, row, box);
    };

    return {
      player,
      game,
      startGame,
      continueGame,
      cashOut,
      selectBox,
      gameresponse,
      cashOut
      
    };
  }
});
</script>
