// src/store/store.js
import { defineStore } from 'pinia';
import axios from 'axios';

export const usePlayerStore = defineStore("GameStore", {
  state: () => ({
    player: { balance: null },
    game: { levels: [] } 
  }),
  actions: {
    async startGame(rows, difficulty, betAmount){   

const requestData = {
  rows: rows,
  difficulty: difficulty,
  betAmount: betAmount
}
   try {
    const response = axios.post('https://localhost:7006/api/Tower/start', requestData );
    setGame(response.data);
  } catch (error) {
    console.error('Error starting game:', error);
  }
    },
  
    async selectBox(  gameId, row, box ) {
      try {
        const response = await axios.post('https://localhost:7006/api/game/select', { gameId, row, box });
        setGame(response.data);
      } catch (error) {
        console.log(error);
        console.error('Error selecting box:', error);
      }
    }
  },
  getters: {
    player: (state) => state.player,
    game: (state) => state.game
  },
  
  setPlayer(state, player) {
    state.player = player;
  },

  setGame(state, game) {
    state.game = game;
  }

});
