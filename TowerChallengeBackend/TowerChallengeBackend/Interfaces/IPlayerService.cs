using TowerChallengeBackend.Models;

namespace TowerChallengeBackend.Interfaces
{
    public interface IPlayerService
    {
        Task<Player> GeneratePlayerAsync();
    }
}