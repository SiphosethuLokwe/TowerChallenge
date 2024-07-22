<template>
  <div>
    <p v-if="player">Balance: {{ player.balance }}</p>
  </div>
</template>

<script>
import { defineComponent, ref, watch } from 'vue';

export default defineComponent({
  props: {
    response: {
      type: Object,
    }
  },
  setup(props) {
    const player = ref(null);

    watch(() => props.response, (newResponse) => {
      if (newResponse && newResponse.data) {
        player.value = newResponse.data.player;
      }
    }, { immediate: true });

    return { player };
  }
});
</script>
