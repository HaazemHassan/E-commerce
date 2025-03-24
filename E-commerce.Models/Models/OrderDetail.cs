using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace E_commerce.Models.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]
        [ValidateNever]
        public Order Order { get; set; }

        [Required]
        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        [ValidateNever]    
        public Product Product { get; set; }

        public int Count { get; set; }
        public double Price { get; set; }

    }
}
