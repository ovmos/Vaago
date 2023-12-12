using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vaago.Models
{
    public class Login
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType("Password")]
        public string Password { get; set; }
    }
}