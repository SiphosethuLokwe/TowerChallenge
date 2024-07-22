using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TowerChallengeBackend.Interfaces;
using TowerChallengeBackend.Models;

namespace TowerChallengeBackend.Services
{
    public class LevelsService : ILevelsService
    {
        private readonly Random _random;

        public LevelsService()
        {
            _random = new Random();
        }

        public async Task<List<Level>> GenerateLevelsAsync(int rows, string difficulty)
        {
            var levels = new List<Level>();

            for (int i = 1; i <= rows; i++)
            {
                var level = new Level
                {
                    RowNumber = i,
                    Boxes = new List<Box>()
                };

                int numBoxes = i + 1;
                int numLossTokens = GetNumLossTokens(difficulty, numBoxes);


                // Create a list of box indices
                var boxIndices = Enumerable.Range(1, numBoxes).ToList();

                // Shuffle the list to randomize the positions of loss tokens
                Shuffle(boxIndices);

                for (int j = 0; j < numBoxes; j++)
                {
                    bool isLossToken = boxIndices[j] <= numLossTokens; // The first numLossTokens elements will be loss tokens
                    level.Boxes.Add(new Box { Id = j + 1, IsLossToken = isLossToken });
                }

                levels.Add(level);

              
            }

            return levels;
        }

        private int GetNumLossTokens(string difficulty, int numBoxes)
        {
            int numLossTokens = difficulty switch
            {
                "easy" => (int)(numBoxes * 0.1),    // 10% for easy
                "medium" => (int)(numBoxes * 0.2),  // 20% for medium
                "hard" => (int)(numBoxes * 0.3),    // 30% for hard
                _ => 0
            };

            // Ensure at least one loss token if the number is greater than zero
            numLossTokens = Math.Max(numLossTokens, 1);

            // Ensure the number of loss tokens does not exceed the number of boxes
            return Math.Min(numLossTokens, numBoxes);
        }

        public decimal WinningsCalculator(string difficulty)
        {
            return difficulty switch
            {
                "easy" => 1.2m,
                "medium" => 1.5m,
                "hard" => 2.0m,
                _ => 1m,
            };
        }

        private void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = _random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
