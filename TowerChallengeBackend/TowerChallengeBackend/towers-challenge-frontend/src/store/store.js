// src/store/store.js
import { defineStore } from 'pinia';
import axios from 'axios';

export const usePlayerStore = defineStore({
  id: 'player',
  state: () => ({
    player: { balance: 1000 },
    game: { levels: [] } // Ensure game is initialized with an empty levels array or appropriate default
  }),
  actions: {
    async startGame({ commit }, { rows, difficulty, betAmount }) {
      try {
        const response = await axios.post('https://localhost:7006/api/game/start', { rows, difficulty, betAmount });
        commit.setGame(response.data);
      } catch (error) {
        console.error('Error starting game:', error);
        // Handle error as needed
      }
    },
    async selectBox({ commit }, { gameId, row, box }) {
      try {
        const response = await axios.post('https://localhost:7006/api/game/select', { gameId, row, box });
        commit.setGame(response.data);
      } catch (error) {
        console.error('Error selecting box:', error);
        // Handle error as needed
      }
    }
  },
  getters: {
    player: (state) => state.player,
    game: (state) => state.game
  },
  mutations: {
    setPlayer(state, player) {
      state.player = player;
    },
    setGame(state, game) {
      state.game = game;
    }
  }
});
