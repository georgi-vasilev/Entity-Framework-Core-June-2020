namespace SalesDatabase.Data.Models
{
    using System.Collections.Generic;
    using SalesDatabase.Data.Models.Common;
    using System.ComponentModel.DataAnnotations;

    public class Store
    {
        public Store()
        {
            this.Sales = new HashSet<Sale>();
        }

        [Key]
        public int StoreId { get; set; }

        [Required]
        [MaxLength(GlobalConstants.StoreNameMaxLength)]
        public string Name { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}
