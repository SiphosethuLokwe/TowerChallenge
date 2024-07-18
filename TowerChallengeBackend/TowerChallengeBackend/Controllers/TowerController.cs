using Microsoft.AspNetCore.Mvc;
using TowerChallengeBackend.DTO;
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
      
        public IActionResult StartGame(RequestDTO requestDTO)
        {
            //Generate random player  
            var player = _playerService.GeneratePlayerAsync().Result;
            if (player?.Balance < requestDTO.BetAmount)
            {
                return BadRequest("Insufficient funds");
            }

           //setup game based on the parameters 
            _games = _gameService.SpinUpGameAsync(requestDTO.Rows, requestDTO.BetAmount, requestDTO.Difficulty);

            //return game 
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
