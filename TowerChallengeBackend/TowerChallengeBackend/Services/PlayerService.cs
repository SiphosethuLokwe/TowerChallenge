using TowerChallengeBackend.Interfaces;
using TowerChallengeBackend.Models;

namespace TowerChallengeBackend.Services
{
    public class PlayerService : IPlayerService
    {

        public PlayerService() { }

        public async Task<List<Player>> GeneratePlayersAsync()
        {
            List<Player> palyerList = new List<Player>();
            var player1 = new Player()
            {
                Id = 1,
                Balance = 1000,
                Name = "Mario",

            };
            palyerList.Add(player1);
            var player2 = new Player()
            {
                Id = 2,
                Balance = 1000,
                Name = "Bawser",
            };
            palyerList.Add(player2);

            var player3 = new Player()
            {
                Id = 3,
                Balance = 1000,
                Name = "Shrek",
            };
            palyerList.Add(player3);

            return palyerList;

        }
    }
}
