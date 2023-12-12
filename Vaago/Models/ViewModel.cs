using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vaago.Models
{
    public class ViewModel
    {
        public Order order { get; set; }
        public Order_Details order_Details { get; set; }
        public Account customer { get; set; }
        public Menu menuItem { get; set; }
    }
}