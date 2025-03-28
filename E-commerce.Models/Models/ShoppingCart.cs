﻿using E_commerce.Models.IdentityEntities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_commerce.Models
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        [ValidateNever]
        public Product Product { get; set; }
        [Range(1, 1000, ErrorMessage = "Please enter a value between 1 and 1000")]
        public int Count { get; set; } = 1;

        public Guid ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        
        [NotMapped]   //won't be created in database
        public double Price { get; set; }
    }
}
