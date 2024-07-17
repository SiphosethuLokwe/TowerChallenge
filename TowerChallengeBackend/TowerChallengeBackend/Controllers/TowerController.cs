using Microsoft.AspNetCore.Mvc;
using TowerChallengeBackend.Interfaces;
using TowerChallengeBackend.Models;

namespace TowerChallengeBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TowerController : Controller
    {
        private readonly IPlayerService _playerService;
        private readonly IGameService _gameService;
        private readonly ILevelsService _levelsService; 
        private  List<Game> _games;

        public TowerController(IPlayerService playerService, IGameService gameService, ILevelsService levelsService )
        {
            _playerService = playerService;
            _gameService = gameService;
            _levelsService = levelsService;
        }

        [HttpPost]
        [Route("start")]
      
        public IActionResult StartGame(int rows, string difficulty, decimal betAmount)
        {
            //take first player 
            var player = _playerService.GeneratePlayersAsync().Result.FirstOrDefault();
            if (player?.Balance < betAmount)
            {
                return BadRequest("Insufficient funds");
            }

            _games = _gameService.SpinUpGameAsync(rows, betAmount, difficulty);

            return Ok(_games);
        }

        [HttpPost]
        [Route("select")]
        public IActionResult SelectBox(int gameId, int row, int box)
        {

          var game = _games.FirstOrDefault(g => g.Id == gameId);

            if (game == null || !game.IsActive)
            {
                return BadRequest("Game not found or not active");
            }
            var selectedBox = game.Levels.First(l => l.RowNumber == row).Boxes.First(b => b.Id == box);

            if (selectedBox.IsLossToken)
            {
                game.IsActive = false;
                return Ok(new { hasLost = true, winnings = 0 });
            }
            else
            {
                game.CurrentWinnings *= _levelsService.GetMultiplier(game.Difficulty);
                return Ok(new { hasLost = false, winnings = game.CurrentWinnings });
            }


        }
    }
}
