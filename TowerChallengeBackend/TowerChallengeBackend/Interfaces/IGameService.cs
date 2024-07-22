using TowerChallengeBackend.Models;

namespace TowerChallengeBackend.Interfaces
{
    public interface IGameService
    {
        Game SpinUpGameAsync(Player player,  int rows, decimal betAmount, string difficulty);
        Game GetCurrentGame();
    }
}