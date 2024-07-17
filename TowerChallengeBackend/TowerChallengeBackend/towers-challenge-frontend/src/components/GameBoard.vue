<template>
    <div>
      <canvas ref="canvas" width="800" height="600"></canvas>
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
        const canvas = this.$refs.canvas;
        const ctx = canvas.getContext('2d');
        
        // Clear canvas
        ctx.clearRect(0, 0, canvas.width, canvas.height);
        
        // Draw levels and boxes
        if (this.levels && this.levels.length > 0) {
          this.levels.forEach((level, rowIndex) => {
            level.boxes.forEach((box, boxIndex) => {
              const x = 50 * boxIndex;
              const y = 50 * rowIndex;
              
              // Draw box
              ctx.fillStyle = '#ffffff';
              ctx.fillRect(x, y, 50, 50);
              
              // Handle box click
              canvas.addEventListener('click', (event) => {
                const rect = canvas.getBoundingClientRect();
                const mouseX = event.clientX - rect.left;
                const mouseY = event.clientY - rect.top;
                
                if (mouseX >= x && mouseX <= x + 50 && mouseY >= y && mouseY <= y + 50) {
                  this.selectBox(level.rowNumber, box.id);
                }
              });
            });
          });
        }
      },
      selectBox(rowId, boxId) {
        this.$emit('selectBox', rowId, boxId);
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
  canvas {
    border: 1px solid #ccc;
  }
  </style>
  