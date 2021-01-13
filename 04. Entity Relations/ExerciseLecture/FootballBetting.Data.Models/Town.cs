namespace FootballBetting.Data.Models
{
    using System.Collections.Generic;

    public class Town
    {
        public Town()
        {
            this.Teams = new HashSet<Team>();
        }

        public int TownId { get; set; }

        public string Name { get; set; }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<Team> Teams { get; set; }
    }
}
