using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Models.DTO
{
    public class ProductUpdateRequest
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 10, ErrorMessage = "Tile must has 10-100 characters")]

        public string Title { get; set; }
        [StringLength(maximumLength: 250, MinimumLength = 10, ErrorMessage = "Tile must has 10-250 characters")]

        public string Description { get; set; }

        [Required]
        public string Author { get; set; }
        [Required]
        [Display(Name = "List Price")]
        [Range(1, 1000)]
        public double ListPrice { get; set; }

        [Required]
        [Display(Name = "Price for 1-50")]
        [Range(1, 1000)]
        public double Price { get; set; }


        [Required]
        [Display(Name = "Price for 50+")]
        [Range(1, 1000)]
        public double Price50 { get; set; }

        [Required]
        [Display(Name = "Price for 100+")]
        [Range(1, 1000)]
        public double Price100 { get; set; }
    }
}

