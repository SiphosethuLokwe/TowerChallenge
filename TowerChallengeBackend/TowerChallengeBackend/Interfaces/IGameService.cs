using TowerChallengeBackend.Models;

namespace TowerChallengeBackend.Interfaces
{
    public interface IGameService
    {
        List<Game> SpinUpGameAsync(int rows, decimal betAmount, string difficulty);
    }
}