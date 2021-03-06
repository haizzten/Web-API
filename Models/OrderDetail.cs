using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace f7.Models
{
    [Table("orderDetail")]
    [Index(nameof(OrderId), Name = "IX_orderDetail_OrderId")]
    public partial class OrderDetailModels
    {
        [Key]
        public string ItemId { get; set; }
        [Key]
        public string OrderId { get; set; }
        public int Quantity { get; set; }

        [ForeignKey(nameof(ItemId))]
        [InverseProperty("OrderDetails")]
        public virtual ItemModels Item { get; set; }
        [ForeignKey(nameof(OrderId))]
        [InverseProperty("OrderDetails")]
        public virtual OrderModels Order { get; set; }
    }
}
