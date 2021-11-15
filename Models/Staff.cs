using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace f7.Models
{
    [Table("staffs")]
    public partial class Staff
    {
        public Staff()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        public string StaffId { get; set; }
        [Required]
        public string StaffName { get; set; }
        public bool Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Position { get; set; }

        [InverseProperty(nameof(Order.Staff))]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
