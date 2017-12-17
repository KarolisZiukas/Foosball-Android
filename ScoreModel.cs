using SQLite;
namespace Foosball_Android
{
    public class ScoreModel 
    {
       [PrimaryKey]
        public long id { get; set; }
        public int redTeamScore { get; set; }
        public int blueTeamScore { get; set; }

    }
}