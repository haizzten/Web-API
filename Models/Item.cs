using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace f7.Models
{
    [Table("items")]
    public partial class Item
    {
        public Item()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public int? WarehouseId { get; set; }

        [InverseProperty(nameof(OrderDetail.Item))]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
