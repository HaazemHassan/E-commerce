using System.ComponentModel.DataAnnotations;

namespace E_commerce_ViewModels
{
    public class OrderSummaryVM
    {
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string StreetAddress { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string Name { get; set; }

        public int Count { get; set; }
        public double Price { get; set; }
    }
}
