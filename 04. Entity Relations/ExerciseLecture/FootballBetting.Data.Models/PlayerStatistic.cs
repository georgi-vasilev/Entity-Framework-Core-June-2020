namespace FootballBetting.Data.Models
{
    public class PlayerStatistic
    {
        //TODO: Composite PK
        public int GameId { get; set; }
        public Game Game { get; set; }

        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }

        public byte ScoredGoals { get; set; }

        public byte Assists { get; set; }

        public byte MinutesPlaying { get; set; }
    }
}
