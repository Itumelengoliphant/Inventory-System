

namespace Store_Inventory_Management_System_
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Institution
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is a required field")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is a required field")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Tel is a required field")]
        public string Tel { get; set; }

        [Required(ErrorMessage = "Mail is a required field")]

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Type is a required field")]
        public string Type { get; set; }


    }
}
