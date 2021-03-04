﻿namespace PetShop.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ClientProduct
    {
        [Required]
        [ForeignKey(nameof(Client))]
        public string ClientId { get; set; }
        public virtual Client Client { get; set; }

        [Required]
        [ForeignKey(nameof(Product))]
        public string ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int Quantity { get; set; }

        [Required]
        [ForeignKey(nameof(Order))]
        public string OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
