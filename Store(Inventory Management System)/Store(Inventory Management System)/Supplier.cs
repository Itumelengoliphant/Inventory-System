
namespace Store_Inventory_Management_System_
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Supplier
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Supplier()
        {
            this.Products = new HashSet<Product>();
        }
    
        public int Id { get; set; }

        [Required(ErrorMessage = "Supplier Name Required!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Speciality Required!")]
        public string Speciality { get; set; }

        [Required(ErrorMessage = "Address Required!")]
        public string Address { get; set; }

        [Required(ErrorMessage = "EMail Required!")]
        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Phone Number Required1")]
        public string Phone { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
