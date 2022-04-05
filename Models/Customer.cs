using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace f7.Models
{
    [Table("customers")]
    public partial class CustomerModels
    {
        [Key]
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        [StringLength(200)]
        public string Address { get; set; }
    }
}
