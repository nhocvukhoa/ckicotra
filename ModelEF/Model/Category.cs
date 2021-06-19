namespace ModelEF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category()
        {
            Products = new HashSet<Product>();
        }

        [Display(Name = "Mã danh mục")]
        public long ID { get; set; }

        [StringLength(100)]
        [Display(Name = "Tên danh mục")]
        [Required(ErrorMessage ="Tên danh mục là bắt buộc")]
        public string Name { get; set; }

        [StringLength(255)]
        [Display(Name = "Mô tả")]
        [Required(ErrorMessage = "Mô tả là bắt buộc")]
        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
