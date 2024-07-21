using System;
using TowerChallengeBackend.Interfaces;
using TowerChallengeBackend.Models;

namespace TowerChallengeBackend.Services
{
    public class GameService : IGameService
    {
        private readonly ILevelsService _levelService;
        private readonly Random _random;


        public GameService(ILevelsService levelsService ) {
            _levelService = levelsService;
            _random = new Random();

        }

        public  Game SpinUpGameAsync(Player player, int rows, decimal betAmount, string difficulty)
        {

            var game = new Game
            {
                Id = _random.Next(50, 10000),
                Rows = rows,
                Difficulty = difficulty,
                BetAmount = betAmount,
                Levels = _levelService.GenerateLevelsAsync(rows, difficulty).Result,
                CurrentWinnings = betAmount,
                player = player
            };
            return game;
        }
    }
}
