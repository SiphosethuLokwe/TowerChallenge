using System.Reflection.Emit;

namespace TowerChallengeBackend.Models
{
    public class Game
    {
        public int Id { get; set; }
        public int Rows { get; set; }
        public string Difficulty { get; set; }
        public decimal BetAmount { get; set; }
        public Player player { get; set; }
        public List<Level> Levels { get; set; }
        public bool IsActive { get; set; } = true;
        public decimal CurrentWinnings { get; set; }
        public bool HasWon { get; set; } = false;
    }
}
