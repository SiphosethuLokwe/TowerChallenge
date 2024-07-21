

<template>
    <div>
      <input v-model.number="rows" placeholder="Rows (4-8)" />
      <input v-model="difficulty" placeholder="Difficulty (easy, medium, hard)" />
      <input v-model.number="betAmount" placeholder="Bet Amount" />
      <button @click="startGame">Start Game</button>
    </div>
</template>
  
  <script>
  export default {
    data() {
      return {
        rows: 4,
        difficulty: 'easy',
        betAmount: 1000
      };
    },
    methods: {
      startGame() {
        this.$emit('startGame', this.rows, this.difficulty, this.betAmount);
      }
    }
  };
  </script>
  