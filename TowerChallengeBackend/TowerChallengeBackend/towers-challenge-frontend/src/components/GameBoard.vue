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
    boxResponse: Object,
    cashout:Boolean
  },
  data() {
    return {
      isEndgame: false 
    };
  },
  mounted() {
    this.setupGame();
  },
  watch: {
    boxResponse: {
      handler(newResponse) {
        this.isEndgame = newResponse.data.isEndgame; 
      },
      
      deep: true
    },
    cashout:{
        handler(cashResponse) {
       console.log(cashResponse);
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
  
    },
    selectBox(rowId, boxId) {
      if (this.isEndgame) return; 

      const level = this.levels.find(level => level.rowNumber === rowId);
      if (!level) return;

      const box = level.boxes.find(box => box.id === boxId);
      if (!box || box.selected) return;

      box.selected = true;

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
  background-color: #d3d3d3; 
  cursor: not-allowed; 
}

.box.disabled {
  background-color: #f0f0f0; 
}
</style>
