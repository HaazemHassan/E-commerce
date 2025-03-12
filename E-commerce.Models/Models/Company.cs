using System.ComponentModel.DataAnnotations;

namespace E_commerce.Models
{
    // I won't make DTO for this model because it's simple
    public class Company
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Company name is required.")]
        public string Name { get; set; }

        [StringLength(50, MinimumLength = 5, ErrorMessage = "{0} must be between 5 and 50 characters.")]
        [Display(Name = "Street Address")]

        public string? StreetAddress { get; set; }

        [StringLength(30, MinimumLength = 2, ErrorMessage = "{0} name must be between 2 and 30 characters.")]
        public string? City { get; set; }

        [StringLength(30, MinimumLength = 2, ErrorMessage = "{0} name must be between 2 and 30 characters.")]
        public string? Country { get; set; }

        [StringLength(30, MinimumLength = 2, ErrorMessage = "{0} name must be between 2 and 30 characters.")]
        public string? State { get; set; }

        [StringLength(30, MinimumLength = 2, ErrorMessage = "{0} must be between 2 and 30 characters.")]
        [Display(Name = "Postal Code")]
        public string? PostalCode { get; set; }

        [RegularExpression(@"^01[0-9]{9}$", ErrorMessage = "{0} must be 11 digits and start with 01.")]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }
    }
}
