using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Models.IdentityEntities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [Required]
        public string PersonName { get; set; }
        public string? City { get; set; }
        public string? StreetAddress { get; set; }
        public string? PostalCode { get; set; }
        public int? CompanyId { get; set; }
        [ValidateNever]
        [ForeignKey(nameof(CompanyId))]
        public Company Company { get; set; }

    }
}
