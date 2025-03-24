using E_commerce.Models;
using E_commerce.Models.IdentityEntities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace E_commerce_ViewModels
{
    public class OrderSummaryVM
    {
        [ValidateNever]
        [Required]
        public Guid ApplicationUserId { get; set; }

        public List<ShoppingCart> ShoppingCartList { get; set; } = new List<ShoppingCart>();

        [Required]
        [Display(Name = "Phone number")]
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
