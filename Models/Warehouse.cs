using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace f7.Models
{
    [Table("warehouse")]
    public partial class WarehouseModels
    {
        [Key]
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public string WarehouseAddress { get; set; }
        public string PhoneNumber { get; set; }
    }
}
