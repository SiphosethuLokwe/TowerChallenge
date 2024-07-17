namespace TowerChallengeBackend.Models
{
    public class Level
    {
        public int Id { get; set; }
        public int RowNumber { get; set; }
        public List<Box> Boxes { get; set; }
    }
}
