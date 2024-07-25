import { defineStore } from 'pinia';
import axios from 'axios';
import {ref, computed } from 'vue'

export const gamestore = defineStore("gamestore", () => {
  

    const game = ref(null);
    const player = ref(null);
    const boxresponse = ref(null);
    const isGameEnded = ref(false);


    const gameDetails = computed(() => game.value);
    const playerDetails = computed(() => player.value);
    const boxGamePlayDetails = computed(() => boxresponse.value);
    const cashoutDetails = computed(()=> isCashout.value);

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


   
     const setGame = (newGame) => {
        game.value = newGame;
        player.value = newGame.player;
     };

     const SelectedBoxResponse = (boxResponse) =>{
        boxresponse.value = boxResponse;
     };

     const cashout = () => {
        game.value = null;
      };

      const restartGame =() =>{
        game.value = null;
        player.value = null;
        boxresponse.value = null;
      }; 
      const evaluateBoxes = (selectedBoxes) => {
        let hasLost = false;
        let currentWinnings = game.value.betAmount;
    
        selectedBoxes.forEach(({ rowId, boxId }) => {
            const level = game.value.levels.find(level => level.rowNumber === rowId);
            const box = level.boxes.find(box => box.id === boxId);
            if (box.isLossToken) {
                hasLost = true;
            } else {
                currentWinnings *= getMultiplier(game.value.difficulty);
            }
        });
    
        isGameEnded.value = hasLost;
        
        boxresponse.value = { 
            data: {
                hasLost: hasLost,
                winnings: hasLost ? 0 : currentWinnings
            }
        };
    };
    
      const getMultiplier = (difficulty) => {
        const multipliers = {
          easy: 1.2,
          medium: 1.5,
          hard: 2.0
        };
        return multipliers[difficulty] || 1;
      };

      
    
    return {
        setGame,
        gameDetails,
        playerDetails,
        startGame,
        selectBox,
        boxGamePlayDetails,
        cashout,
        cashoutDetails,
        restartGame,
        evaluateBoxes
    }

})