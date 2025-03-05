using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace E_commerce.Models.DTO
{
    public class CategoryAddRequest
    {

        [Required(ErrorMessage = "Enter category name")]
        [StringLength(maximumLength: 15, MinimumLength = 4, ErrorMessage = "Category name must be between 4 and 15 characters")]
        [Remote(action: "IsCategoryNameNotExists", controller: "Category",
            ErrorMessage = "Name is Already taken")]
        public string Name { get; set; }
        [Display(Name = "Display Order")]
        [Range(1, 100, ErrorMessage = "Display order must be between 1 and 100")]
        public int DisplayOrder { get; set; }


    }
}
