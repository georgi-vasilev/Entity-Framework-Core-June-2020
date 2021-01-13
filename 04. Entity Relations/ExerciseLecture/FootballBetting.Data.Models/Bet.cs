namespace FootballBetting.Data.Models
{
    using System;
    using FootballBetting.Data.Models.Enumerations;

    public class Bet
    {
        public int BetId { get; set; }

        public decimal Amount { get; set; }

        public Prediction Prediction { get; set; }

        public DateTime DateTime { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int GameId { get; set; }
        public virtual Game Game { get; set; }
    }
}
