
namespace Store_Inventory_Management_System_
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ProductOut
    {
        public int Id { get; set; }

        [Display(Name = "Product")]
        [Required(ErrorMessage = "Product Required")]
        public int ProdId { get; set; }

        [Display(Name = "User")]
        [Required(ErrorMessage = "User Required")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Quantity Required")]
        public int Quantity { get; set; }
        public System.DateTime Date { get; set; }

        [Required(ErrorMessage = "Reason Required")]
        public string Reason { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
