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
        private  Game _games;

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
            //Generate random player 
            if (requestDTO.BetAmount == 0)
            {
                return BadRequest("No bet amount provided");

            }
            var player = _playerService.GeneratePlayerAsync().Result;

            if (player?.Balance < requestDTO.BetAmount)
            {
                return BadRequest("Insufficient funds");
            }
            //setup game based on the parameters 
            _games = _gameService.SpinUpGameAsync(player, requestDTO.Rows, requestDTO.BetAmount, requestDTO.Difficulty);
            //return game 
            return Ok(_games);
        }

        [HttpPost]
        [Route("select")]
        public IActionResult SelectBox(int gameId, int row, int box)
        {
            if (gameId <= 0)
            {
                return BadRequest("Game does not exist");
            }

            return Ok(_boxService.GetSelectedBox(ref _games, gameId, row, box));

        }
    }
}
