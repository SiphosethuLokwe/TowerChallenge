<!-- src/App.vue or any other component -->
<template>
  <div>
    <PlayerStats :player="player" />
    <BetPanel @startGame="startGame" />
    <GameBoard v-if="game" :levels="game.levels" :game="game" @selectBox="selectBox" />
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
      console.log(rows, difficulty, betAmount);
      gameStore.startGame(rows, difficulty, betAmount);
    };
     
    const selectBox = (row, box) => {
      gameStore.selectBox({ gameId: game.id, row, box });
    };
      console.log(player);
    onMounted(async () => {
     console.log(player);
     console.log(game);


    });

    return {
    
      player,
      game,
      startGame,
      selectBox
    };
  },
  


});
</script>
