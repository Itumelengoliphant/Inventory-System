
namespace Store_Inventory_Management_System_
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ItemReturn
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProdId { get; set; }
        public int DeptTakenId { get; set; }
        public int DeptRetId { get; set; }

        [Required(ErrorMessage = "Quantity is a required field")]
        public int Quantity { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime Date { get; set; }
        public string Condition { get; set; }
        public virtual Department Department { get; set; }
        public virtual Department Department1 { get; set; }
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
