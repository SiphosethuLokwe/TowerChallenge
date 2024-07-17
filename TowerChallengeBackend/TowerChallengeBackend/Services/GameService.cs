using TowerChallengeBackend.Interfaces;
using TowerChallengeBackend.Models;

namespace TowerChallengeBackend.Services
{
    public class GameService : IGameService
    {
        private readonly ILevelsService _levelService;
        public GameService(ILevelsService levelsService ) {
            _levelService = levelsService;
        }

        public  List<Game> SpinUpGameAsync(int rows, decimal betAmount, string difficulty)
        {

            var games = new List<Game>();
            var game = new Game
            {
                Id = games.Count + 1,
                Rows = rows,
                Difficulty = difficulty,
                BetAmount = betAmount,
                Levels = _levelService.GenerateLevelsAsync(rows, difficulty).Result,
                CurrentWinnings = betAmount
            };
            games.Add(game);
            return games;
        }
    }
}
