namespace ModelEF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserAccount")]
    public partial class UserAccount
    {
        [Display(Name = "ID")]
        public long ID { get; set; }

        [StringLength(50)]
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage ="Tên đăng nhập là bắt buộc")]
        public string Username { get; set; }

        [StringLength(32)]
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        public string Password { get; set; }

        [StringLength(30)]
        [Display(Name = "Trạng thái")]
        public string Status { get; set; }
    }
}
