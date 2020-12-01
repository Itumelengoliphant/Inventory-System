using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store_Inventory_Management_System_.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public System.DateTime Date { get; set; }
        public string Reason { get; set; }

    }
}