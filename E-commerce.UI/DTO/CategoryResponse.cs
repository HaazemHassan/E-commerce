﻿using E_commerce.UI.Models;
using System.ComponentModel.DataAnnotations;

namespace E_commerce.UI.DTO
{
    public class CategoryResponse
    {

        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter category name")]
        [StringLength(maximumLength: 15, MinimumLength = 4, ErrorMessage = "Category name must be between 4 and 15 characters")]

        public string Name { get; set; }
        [Range(1, 100, ErrorMessage = "Display order must be between 1 and 100")]
        public int DisplayOrder { get; set; }
    }



}
