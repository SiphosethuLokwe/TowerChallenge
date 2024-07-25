<template>
  <div class="container mt-3">
    <div class="button-group mb-3">
      <button class="btn btn-primary" @click="setPlayMode('manual')" :disabled="playMode === 'manual'">
        Manual Play
      </button>
      <button class="btn btn-secondary" @click="setPlayMode('auto')" :disabled="playMode === 'auto'">
        Auto Play
      </button>
    </div>
    <div class="game-board">
      <div v-for="(level, rowIndex) in levels" :key="rowIndex" class="row">
        <div
          v-for="(box, boxIndex) in level.boxes"
          :key="box.id"
          class="box"
          :class="boxClasses(level.rowNumber, box)"
          @click="selectBox(level.rowNumber, box.id)"
        >
          {{ box.id }}
        </div>
      </div>
    </div>
    <button
      v-if="playMode === 'auto' && !isGameEnded"
      @click="autoPlay"
      class="btn btn-primary mt-3"
      :disabled="selectedBoxes.length === 0 || isGameEnded"
    >
      Evaluate Selected Boxes
    </button>
  </div>
</template>

<script>
export default {
  props: {
    levels: Array,
    game: Object,
    isGameEnded: Boolean
  },
  data() {
    return {
      selectedBoxes: [],
      playMode: 'manual' // default play mode is manual
    };
  },
  methods: {
    selectBox(rowId, boxId) {
      if (!this.isGameEnded) {
        if (this.playMode === 'manual') {
          if (!this.isBoxSelected(rowId, boxId)) {
            this.$emit('selectBox', this.game.id, rowId, boxId);
            this.selectedBoxes.push({ rowId, boxId });
          }
        } else if (this.playMode === 'auto') {
          const index = this.selectedBoxes.findIndex(
            box => box.rowId === rowId && box.boxId === boxId
          );
          if (index === -1) {
            this.selectedBoxes.push({ rowId, boxId });
          }
        }
      }
    },
    isSelectedBox(rowId, boxId) {
      return this.selectedBoxes.some(box => box.rowId === rowId && box.boxId === boxId);
    },
    isBoxSelected(rowId, boxId) {
      return this.selectedBoxes.some(box => box.rowId === rowId && box.boxId === boxId);
    },
    boxClasses(rowId, box) {
      if (this.playMode === 'auto') {
        // Differentiate selected boxes in auto-play mode
        return {
          'box': true,
          'auto-play-selected-box': this.isSelectedBox(rowId, box.id),
          'auto-play-box': !this.isSelectedBox(rowId, box.id)
        };
      } else {
        // Apply colors based on selection and box type in manual mode
        return {
          'box': true,
          'selected-box': this.isSelectedBox(rowId, box.id),
          'loss-box': box.isLossToken && this.isSelectedBox(rowId, box.id)
        };
      }
    },
    autoPlay() {
      console.log(this.selectedBoxes);
      this.$emit('autoPlay', this.selectedBoxes);
    },
    setPlayMode(mode) {
      this.playMode = mode;
      this.selectedBoxes = [];
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

.auto-play-box {
  background-color: #e0e0e0; /* Gray for non-selected boxes in auto-play mode */
}

.auto-play-selected-box {
  background-color: #b0bec5; /* Slightly darker gray for selected boxes in auto-play mode */
}

.selected-box {
  background-color: #5cb85c; /* Green for selected boxes in manual mode */
}

.loss-box {
  background-color: #d9534f; /* Red for loss boxes in manual mode */
}

.button-group {
  display: flex;
  justify-content: center;
  gap: 10px;
}

.disabled {
  pointer-events: none;
}
</style>
