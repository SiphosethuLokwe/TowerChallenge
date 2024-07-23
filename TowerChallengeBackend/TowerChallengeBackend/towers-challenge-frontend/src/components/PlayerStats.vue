<template>
  <div class="player-stats">
    <h3>Player Stats</h3>
    <p>Has Lost: {{ stats.data.hasLost ? 'Yes' : 'No' }}</p>
    <p>Winnings: {{ stats.data.winnings }}</p>
  </div>
</template>

<script>
export default {
  props: {
    stats: {
      type: Object,
      required: true
    }
  }
};
</script>

<style>
.player-stats {
  border: 1px solid #ccc;
  padding: 10px;
  background-color: #f9f9f9;
}
</style>
