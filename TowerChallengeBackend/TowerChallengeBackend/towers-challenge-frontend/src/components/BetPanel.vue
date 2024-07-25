<template>
  <div>
    <div v-if="!gameResponse">
     <input v-model.number="rows" placeholder="Rows (4-8)" />
    <input v-model="difficulty" placeholder="Difficulty (easy, medium, hard)" />
    <input v-model.number="betAmount" placeholder="Bet Amount" />
    <button @click="startGame" v-if="!gameStarted">Start Game</button>
    </div>
  
    <button @click="restartGame" v-if="gameStarted">Restart Game</button>
    <button @click="cashOut" v-if="gameResponse && !hasLost">Cash Out</button>
    <!-- <button @click="autoPlay" v-if = "gameStarted && !haslost">Start AutoPlay</button> -->

    <PlayerStats v-if="gameResponse" :stats="gameResponse" /> 
  </div>
</template>
  
<script>
import PlayerStats from './PlayerStats.vue';

export default {
  components: {
    PlayerStats
  },
  data() {
    return {
      rows: 4,
      difficulty: 'easy',
      betAmount: 1000,
      gameStarted: false,
      hasLost: false,
      autoPlayCount: 1 
    };
  },
  props: {
    gameResponse: Object
  },
  watch: {
    gameResponse: {
      immediate: true,
      handler(newResponse) {      
        if (newResponse) {
          console.log(newResponse);
          this.hasLost = newResponse.data.hasLost;
          if (newResponse.isEndgame) {
            this.gameStarted = false;
          }
        }
      }
    }
  },
  methods: {
    startGame() {
      this.gameStarted = true;
      this.$emit('startGame', this.rows, this.difficulty, this.betAmount);
    },
    restartGame() {
      this.gameStarted = false;
      this.$emit('restartGame');
    },
    cashOut() {
      this.$emit('cashOut');
    },

  }
};
</script>
