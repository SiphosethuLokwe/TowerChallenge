// src/store/store.js
import { defineStore } from 'pinia';
import axios from 'axios';

export const usePlayerStore = defineStore("GameStore", {
  state: () => {
    return { player: null,
    game: null
    }
  },

 
  actions: {
    async startGame(rows, difficulty, betAmount){   

const requestData = {
  rows: rows,
  difficulty: difficulty,
  betAmount: betAmount
}
   try {
    const response = axios.post('https://localhost:7006/api/Tower/start', requestData )
    .then(response => {
      this.setGame(response.data);
    });
  } catch (error) {
    console.error('Error starting game:', error);
  }
    },
  
    async selectBox(  gameId, row, box ) {
      const requestData = {
        gameId: gameId,
        row: row,
        box: box
      }

      try {
        const response = await axios.post('https://localhost:7006/api/game/select', requestData);
        setGame(response.data);
      } catch (error) {
        console.log(error);
        console.error('Error selecting box:', error);
      }
    },
    setGame(game){
      usePlayerStore.game = game;
      console.log(usePlayerStore.game);
      this.setPlayer(game.player);

    },
    setPlayer(player){
      usePlayerStore.player = player;
      console.log(player);
    },
  },
  getters: {
    player: (state) => state.player,
    game: (state) => state.game
  },
  
 
 

});
