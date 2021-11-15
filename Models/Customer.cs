using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace f7.Models
{
    [Table("customers")]
    public partial class Customer
    {
        [Key]
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        [StringLength(200)]
        public string Address { get; set; }
    }
}
