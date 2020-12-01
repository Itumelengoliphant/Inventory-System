
namespace Store_Inventory_Management_System_
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;


    public partial class Laptop
    {
        public int Id { get; set; }

        [Display(Name = "Brand Name")]
        [Required(ErrorMessage = "Brand Name Required")]
        public string BrandName { get; set; }

        [Display(Name = "Model No")]
        [Required(ErrorMessage = "Model No Required")]
        public string ModelNo { get; set; }

        [Display(Name = "Serial No")]
        [Required(ErrorMessage = "Serial No Required")]
        public string SerialNo { get; set; }

        [Required(ErrorMessage = "Quantity Required")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Condition Required")]
        public string Condition { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
