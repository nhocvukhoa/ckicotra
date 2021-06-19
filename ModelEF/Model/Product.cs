namespace ModelEF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [Display(Name = "Mã sản phẩm")]
        public long ID { get; set; }

        [StringLength(250)]
       // [Required(ErrorMessage ="Tên sản phẩm là bắt buộc")]
        [Display(Name = "Tên sản phẩm")]
      
        public string Name { get; set; }

        [Display(Name = "Đơn giá")]
        //[Required(ErrorMessage = "Đơn giá là bắt buộc")]
        [DisplayFormat(DataFormatString = "{0:0,0} đ")]
        public decimal? UnitCost { get; set; }

        [Display(Name = "Số lượng")]
        //[Required(ErrorMessage = "Số lượng là bắt buộc")]
        public int? Quantity { get; set; }

        [StringLength(250)]
        [Display(Name = "Hình ảnh")]
        //[Required(ErrorMessage = "Hình ảnh là bắt buộc")]
        public string Image { get; set; }

        [StringLength(255)]
        [Display(Name = "Mô tả")]
        //[Required(ErrorMessage = "Mô tả là bắt buộc")]
        public string Description { get; set; }

        [Display(Name = "Trạng thái")]
        //[Required(ErrorMessage = "Trạng thái là bắt buộc")]
        public int? Status { get; set; }

        [Display(Name = "Loại sản phẩm")]
        //[Required(ErrorMessage = "Loại sản phẩm là bắt buộc")]
        public long? ProductType { get; set; }

        public virtual Category Category { get; set; }
    }
}
