using TowerChallengeBackend.Interfaces;
using TowerChallengeBackend.Models;

namespace TowerChallengeBackend.Services
{
    public class LevelsService : ILevelsService
    {
        public LevelsService() { }

        public async Task<List<Level>> GenerateLevelsAsync(int rows, string difficulty)
        {
            var levels = new List<Level>();
            var random = new Random();

            for (int i = 1; i <= rows; i++)
            {
                var level = new Level
                {
                    RowNumber = i,
                    Boxes = new List<Box>()
                };

                int numBoxes = i + 1;
                int numLossTokens = GetNumLossTokens(difficulty, numBoxes);

                for (int j = 1; j <= numBoxes; j++)
                {
                    level.Boxes.Add(new Box { Id = j, IsLossToken = numLossTokens > 0 && random.NextDouble() < (double)numLossTokens / numBoxes });
                    numLossTokens -= level.Boxes.Last().IsLossToken ? 1 : 0;
                }

                levels.Add(level);
            }

            return levels;
        }


        private int GetNumLossTokens(string difficulty, int numBoxes)
        {
            return difficulty switch
            {
                "easy" => (int)(numBoxes * 0.1),
                "medium" => (int)(numBoxes * 0.2),
                "hard" => (int)(numBoxes * 0.3),
                _ => 0,
            };
        }

        public decimal GetMultiplier(string difficulty)
        {
            return difficulty switch
            {
                "easy" => 1.2m,
                "medium" => 1.5m,
                "hard" => 2.0m,
                _ => 1m,
            };
        }
    }
}
