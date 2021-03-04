﻿namespace PetShop.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Breed
    {
        public Breed()
        {
            this.Pets = new HashSet<Pet>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        public string Name { get; set; }

        public virtual ICollection<Pet> Pets { get; set; }
    }
}
