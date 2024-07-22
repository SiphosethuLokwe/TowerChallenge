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
    game: Object
  },
  mounted() {
    this.setupGame();
  },
  methods: {
    setupGame() {
      console.log('Levels:', this.levels);
      console.log('Game:', this.game);
    },
    selectBox(rowId, boxId) {
      console.log(rowId,boxId,this.game.id);
      this.$emit('selectBox',this.game.id, rowId, boxId);
    }
  },
  watch: {
    levels: {
      handler() {
        this.setupGame();
      },
      deep: true // Watch for changes in nested arrays
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
</style>
