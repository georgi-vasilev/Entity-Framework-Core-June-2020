using System.Collections.Generic;

namespace FootballBetting.Data.Models
{
    public class Country
    {
        public Country()
        {
            this.Towns = new HashSet<Town>();
        }

        public int CountryId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Town> Towns { get; set; }
    }
}
