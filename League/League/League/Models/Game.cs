using SQLite;

namespace League.Models
{
    [Table("Game")]
    public class Game
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int HomeTeam { get; set; }
        public int AwayTeam { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public bool Completed { get; set; }
        public int Week { get; set; }
    }
}