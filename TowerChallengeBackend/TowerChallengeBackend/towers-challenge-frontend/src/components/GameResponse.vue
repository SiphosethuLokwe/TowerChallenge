<!-- BoxResponseDisplay.vue -->
<template>
    <div>
      <el-alert
        v-if="response"
        :title="alertTitle"
        :type="alertType"
        :description="responseDescription"
        show-icon
      />
    </div>
  </template>
  
  <script>
  import { defineComponent, ref, watch } from 'vue';
  import { ElAlert } from 'element-plus';
  
  export default defineComponent({
    components: { ElAlert },
    props: {
      response: {
        type: Object,
        required: true
      }
    },
    setup(props) {
      const alertTitle = ref('');
      const alertType = ref('');
      const responseDescription = ref('');
  
      watch(() => props.response, (newResponse) => {
        console.log(newResponse.data);
        if (newResponse.data.hasLost) {
          alertTitle.value = 'Game Over';
          alertType.value = 'error';
          responseDescription.value = 'Sorry, you have lost. Better luck next time!';
        } else {
          alertTitle.value = 'Congratulations!';
          alertType.value = 'success';
          responseDescription.value = `Your new balance is now ${newResponse.data.winnings}`;
        }
      });
  
      return {
        alertTitle,
        alertType,
        responseDescription
      };
    }
  });
  </script>
  
  <style scoped>
  .el-alert {
    margin: 20px 0;
  }
  </style>
  