using TowerChallengeBackend.Models;

namespace TowerChallengeBackend.Interfaces
{
    public interface IBoxService
    {
        BoxResponse GetSelectedBox(ref Game games, int gameId, int row, int box);
    }
}