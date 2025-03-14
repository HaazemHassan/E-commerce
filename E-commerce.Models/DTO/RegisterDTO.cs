﻿using E_commerce.Models.Enums;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace E_commerce.Models.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "{0} can't be blank")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Remote(action: "IsEmailNotExists", controller: "Account", ErrorMessage = "Email is already taken")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} can't be blank")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0} can't be blank")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "{0} can't be blank")]
        [Display(Name = "Person Name")]
        public string PersonName { get; set; }

        [RegularExpression(@"^01[0-9]{9}$", ErrorMessage = "Invalid phone number format")]
        public string? PhoneNumber { get; set; }
        public string? City { get; set; }
        public string? StreetAddress { get; set; }
        public string? PostalCode { get; set; }
        public Roles Role { get; set; }
        public int? CompanyId { get; set; }

    }
}
