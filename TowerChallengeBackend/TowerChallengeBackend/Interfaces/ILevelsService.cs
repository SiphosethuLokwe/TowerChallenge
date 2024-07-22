using TowerChallengeBackend.Models;

namespace TowerChallengeBackend.Interfaces
{
    public interface ILevelsService
    {
        Task<List<Level>> GenerateLevelsAsync(int rows, string difficulty);
        decimal WinningsCalculator(string difficulty);
    }
}