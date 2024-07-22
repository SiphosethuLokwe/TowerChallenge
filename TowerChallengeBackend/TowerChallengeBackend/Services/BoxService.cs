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
        public BoxResponse GetSelectedBox( Game game, int row, int box)
        {

            var selectedBox = game.Levels.First(l => l.RowNumber == row).Boxes.First(b => b.Id == box);

            if (selectedBox.IsLossToken)
            {
                return new BoxResponse { hasLost = true, Winnings = 0,  isEndgame = true};
            }
            else
            {
                game.CurrentWinnings *= _levelsService.WinningsCalculator(game.Difficulty);
                return new BoxResponse { hasLost = false, Winnings = game.CurrentWinnings };
            }
        }
    }
}
