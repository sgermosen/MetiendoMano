using SQLite;

namespace League.Models
{
    [Table("Team")]
    public class Team
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
