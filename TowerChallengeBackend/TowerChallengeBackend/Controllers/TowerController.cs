using Microsoft.AspNetCore.Mvc;
using TowerChallengeBackend.DTO;
using TowerChallengeBackend.Interfaces;
using TowerChallengeBackend.Models;
using TowerChallengeBackend.Services;

namespace TowerChallengeBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TowerController : Controller
    {
        private readonly IPlayerService _playerService;
        private readonly IGameService _gameService;
        private readonly IBoxService _boxService;

        public TowerController(IPlayerService playerService, IGameService gameService, ILevelsService levelsService, IBoxService boxService )
        {
            _playerService = playerService;
            _gameService = gameService;
            _boxService = boxService;
        }

        [HttpPost]
        [Route("start")]
      
        public IActionResult StartGame(RequestDTO requestDTO)
        {
            if (requestDTO.BetAmount == 0)
            {
                return BadRequest("No bet amount provided");

            }
            var player = _playerService.GeneratePlayerAsync().Result;

            if (player?.Balance < requestDTO.BetAmount)
            {
                return BadRequest("Insufficient funds");
            }
           
            return Ok(_gameService.SpinUpGameAsync(player, requestDTO.Rows, requestDTO.BetAmount, requestDTO.Difficulty));
        }

        [HttpPost]
        [Route("select")]
        public IActionResult SelectBox(BoxSelectDTO boxSelectDTO)
        {
            if (boxSelectDTO.GameId <= 0)
            {
                return BadRequest("Game does not exist");
            }
            var currentGame = _gameService.GetCurrentGame();

            if (currentGame.Id != boxSelectDTO.GameId)
            {
                return BadRequest("Something went wrong with the intance of the game");
            }
            return Ok(_boxService.GetSelectedBox(currentGame, boxSelectDTO.Row, boxSelectDTO.Box));

        }
    }
}
