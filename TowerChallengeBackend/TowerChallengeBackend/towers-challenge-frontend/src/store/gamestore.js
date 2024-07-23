import { defineStore } from 'pinia';
import axios from 'axios';
import {ref, computed } from 'vue'

export const gamestore = defineStore("gamestore", () => {
  

    const game = ref(null);
    const player = ref(null);
    const boxresponse = ref(null);
    const isCashout = ref(null);

    const gameDetails = computed(() => game.value);
    const playerDetails = computed(() => player.value);
    const boxGamePlayDetails = computed(() => boxresponse.value);
    const cashoutDetails = computed(()=> isCashout.value);
    console.log(cashoutDetails.value);

    const startGame = (rows, difficulty, betAmount) => {
        const requestData = {
            rows: rows,
            difficulty: difficulty,
            betAmount: betAmount
          }
          console.log(requestData);

             try {
              const response = axios.post('https://localhost:7006/api/Tower/start', requestData )
              .then(response => {
                setGame(response.data);
              });
            } catch (error) {
              console.error('Error starting game:', error);
            }
    };

    const selectBox = ( gameId, row, box )  => {
        const requestData = {
          gameId: gameId,
          row: row,
          box: box
        }
  
        try {
           axios.post('https://localhost:7006/api/Tower/select', requestData)
          .then(response => {
            console.log(response);
            SelectedBoxResponse(response);
          });
        } catch (error) {
          console.log(error);
          console.error('Error selecting box:', error);
        }
      };

      const startAutoPlay = (rows, difficulty, betAmount, count) => {
        const requestData = { rows, difficulty, betAmount, count };
        try {
          const response = axios.post('https://localhost:7006/api/Tower/startAutoPlay', requestData);
          setGame(response.data);
        } catch (error) {
          console.error('Error starting autoplay:', error);
        }
      };
   
     const setGame = (newGame) => {
        game.value = newGame;
        player.value = newGame.player;

     };
     const SelectedBoxResponse = (boxResponse) =>{
        boxresponse.value = boxResponse;
     };

     const cashout = (iscashout) => {
        isCashout.value = iscashout;  
      };
    
    return {
        setGame,
        gameDetails,
        playerDetails,
        startGame,
        selectBox,
        boxGamePlayDetails,
        cashout,
        cashoutDetails
    }

})