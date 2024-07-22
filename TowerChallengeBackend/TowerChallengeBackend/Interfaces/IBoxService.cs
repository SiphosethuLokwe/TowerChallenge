using TowerChallengeBackend.Models;

namespace TowerChallengeBackend.Interfaces
{
    public interface IBoxService
    {
        BoxResponse GetSelectedBox(Game game, int row, int box);
    }
}