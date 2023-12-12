using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vaago.Models
{
    public class CheckoutModel
    {
        public Menu menuItem { get; set; }
        public Cart cartItem { get; set; }
        public Account user { get; set; }     
        
    }
}