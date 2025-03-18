using System.ComponentModel.DataAnnotations;

namespace E_commerce_ViewModels
{
    public class OrderSummaryVM
    {
        [Required]
        [Display(Name="Phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Street address")]
        public string StreetAddress { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [Display(Name = "Postal code")]
        public string PostalCode { get; set; }

        [Required]
        public string PersonName { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
    }
}
