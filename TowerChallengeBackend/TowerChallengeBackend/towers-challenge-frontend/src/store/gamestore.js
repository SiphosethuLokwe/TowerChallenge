import { defineStore } from 'pinia';
import axios from 'axios';
import {ref, computed } from 'vue'

export const gamestore = defineStore("gamestore", () => {
    const state = ref({
        game :null,
        player: null
    });

    const game = ref(null);
    const player = ref(null);

    const gameDetails = computed(() => game.value);
    const playerDetails = computed(() => player.value);

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
   
     const setGame = (newGame) => {
        game.value = newGame;
        player.value = newGame.player;

     };
    
    return {
        state,
        setGame,
        gameDetails,
        playerDetails,
        startGame
    }

})