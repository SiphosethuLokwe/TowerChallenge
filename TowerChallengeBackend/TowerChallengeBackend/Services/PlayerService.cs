using TowerChallengeBackend.Interfaces;
using TowerChallengeBackend.Models;

namespace TowerChallengeBackend.Services
{
    public class PlayerService : IPlayerService
    {
        private static readonly string[] Names = new string[]
        {
            "Mario", "Luigi", "Peach", "Bowser", "Yoshi", "Toad", "Donkey Kong", "Wario", "Link", "Zelda", "Samus", "Kirby"
        };

        private readonly Random _random;

        public PlayerService() {
            _random = new Random();
        }

        public async Task<Player> GeneratePlayerAsync()
        {
           //simulate getting a player  from somewhere , here just randomizing a random player when staring a game 
                var player = new Player()
                {
                    Id = _random.Next(1, 1000),
                    Balance = 1000,
                    Name = Names[_random.Next(Names.Length)],
                };      

            return player;

        }

      
    }
}
