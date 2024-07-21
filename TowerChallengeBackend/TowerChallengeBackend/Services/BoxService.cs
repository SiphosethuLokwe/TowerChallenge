using TowerChallengeBackend.Interfaces;
using TowerChallengeBackend.Models;

namespace TowerChallengeBackend.Services
{
    public class BoxService : IBoxService
    {
        public readonly ILevelsService _levelsService;

        public BoxService(ILevelsService levelsService)
        {
            _levelsService = levelsService;
        }
        public BoxResponse GetSelectedBox(ref Game games, int gameId, int row, int box)
        {
            var game = games;

            var selectedBox = game.Levels.First(l => l.RowNumber == row).Boxes.First(b => b.Id == box);

            if (selectedBox.IsLossToken)
            {
                game.IsActive = false;
                return new BoxResponse { hasLost = true, Winnings = 0 };
            }
            else
            {
                game.CurrentWinnings *= _levelsService.GetMultiplier(game.Difficulty);
                return new BoxResponse { hasLost = false, Winnings = game.CurrentWinnings };
            }
        }
    }
}
