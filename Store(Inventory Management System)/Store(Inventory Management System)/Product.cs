
namespace Store_Inventory_Management_System_
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.ItemReturns = new HashSet<ItemReturn>();
            this.ProductOuts = new HashSet<ProductOut>();
        }
    
        public int Id { get; set; }

        [Required(ErrorMessage = "Name Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description Required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Quantity Required")]
        public int Quantity { get; set; }

        public decimal Cost { get; set; }

        [Display(Name = "Does Expire?")]
        [Required(ErrorMessage = "Expiry Info Required")]
        public bool DoesExpire { get; set; }
        public int CatId { get; set; }
        public int DepId { get; set; }
        public int SuppId { get; set; }

        [Required(ErrorMessage = "Condition Required")]
        public string Condition { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Department Department { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ItemReturn> ItemReturns { get; set; }
        public virtual Supplier Supplier { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductOut> ProductOuts { get; set; }
    }
}
