﻿namespace RealEstates.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class RealEstateProperty
    {
        public RealEstateProperty()
        {
            this.Tags = new HashSet<RealEstatePropertyTag>();
        }

        public int Id { get; set; }

        [Required]
        public int Size { get; set; }

        public int? Floor { get; set; }

        public int? TotalNumberOfFloors { get; set; }

        public int DistrictId { get; set; }
        public virtual District District { get; set; }

        public int? Year { get; set; }

        public int PropertyTypeId { get; set; }
        public virtual PropertyType PropertyType { get; set; }

        public int BuildingTypeId { get; set; }

        public virtual BuildingType BuildingType { get; set; }

        public int Price { get; set; }

        public virtual ICollection<RealEstatePropertyTag> Tags { get; set; }
    }
}
