<template>
  <div>
    <div class="game-board">
      <div
        v-for="(level, rowIndex) in levels"
        :key="rowIndex"
        class="row"
      >
        <div
          v-for="(box, boxIndex) in level.boxes"
          :key="box.id"
          class="box"
          :class="{ selected: box.selected, disabled: isEndgame }"
          @click="selectBox(level.rowNumber, box.id)"
        >
          {{ box.id }}
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    levels: Array,
    game: Object,
    boxResponse: Object // Add the boxResponse prop to handle response
  },
  data() {
    return {
      isEndgame: false // Default value for isEndgame
    };
  },
  mounted() {
    this.setupGame();
  },
  watch: {
    boxResponse: {
      handler(newResponse) {
        console.log(newResponse);
        this.isEndgame = newResponse.data.isEndgame; // Update isEndgame based on the response
      },
      deep: true
    },
    levels: {
      handler() {
        this.setupGame();
      },
      deep: true
    }
  },
  methods: {
    setupGame() {
      console.log('Levels:', this.levels);
      console.log('Game:', this.game);
      console.log('BoxResponse:', this.boxResponse);
    },
    selectBox(rowId, boxId) {
      if (this.isEndgame) return; // Do nothing if the game is in end state

      // Find the level and box
      const level = this.levels.find(level => level.rowNumber === rowId);
      if (!level) return;

      const box = level.boxes.find(box => box.id === boxId);
      if (!box || box.selected) return;

      // Mark box as selected
      box.selected = true;

      console.log(rowId, boxId, this.game.id);
      this.$emit('selectBox', this.game.id, rowId, boxId);
    }
  }
};
</script>

<style>
.game-board {
  display: flex;
  flex-direction: column;
}

.row {
  display: flex;
}

.box {
  width: 50px;
  height: 50px;
  margin: 2px;
  background-color: #ffffff;
  border: 1px solid #ccc;
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
}

.box.selected {
  background-color: #d3d3d3; /* Change color to indicate selection */
  cursor: not-allowed; /* Change cursor to indicate disabled */
}

.box.disabled {
  cursor: not-allowed; /* Disable cursor if the game is in end state */
  background-color: #f0f0f0; /* Optional: Change color to indicate disabled state */
}
</style>
