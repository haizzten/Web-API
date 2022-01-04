using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace f7.Models
{
    [Table("staffs")]
    public partial class StaffModels
    {
        public StaffModels()
        {
            Orders = new HashSet<OrderModels>();
        }

        [Key]
        public string StaffId { get; set; }
        [StringLength(50, ErrorMessage = "Tên tối đa 50 ký tự")]
        [Required]
        [Display(Name = "Tên nhân viên")]
        public string StaffName { get; set; }
        [Display(Name = "Giới tính")]
        public bool Gender { get; set; }
        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; }
        [Display(Name = "Địa chỉ")]
        [StringLength(200, ErrorMessage = "Địa chỉ tối đa 200 ký tự")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "Chức vụ")]
        public string Position { get; set; }

        [InverseProperty(nameof(OrderModels.Staff))]
        public virtual ICollection<OrderModels> Orders { get; set; }
    }
}
