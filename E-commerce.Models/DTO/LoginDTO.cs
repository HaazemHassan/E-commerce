using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Models.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "{0} can't be blank")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} can't be blank")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
