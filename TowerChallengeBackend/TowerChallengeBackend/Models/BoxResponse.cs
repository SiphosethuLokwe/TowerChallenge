using System.Diagnostics.Contracts;

namespace TowerChallengeBackend.Models
{
    public class BoxResponse
    {
        public bool hasLost {  get; set; }
        public decimal Winnings { get; set; }

        public bool isEndgame { get; set; } = false;
    }
}
