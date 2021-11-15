using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace f7.Models
{
    [Table("orders")]
    [Index(nameof(StaffId), Name = "IX_orders_StaffId")]
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        public string OrderId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CustomerId { get; set; }
        public string PaymentMethod { get; set; }
        public int? VatPercent { get; set; }
        public string StaffId { get; set; }

        [ForeignKey(nameof(StaffId))]
        [InverseProperty("Orders")]
        public virtual Staff Staff { get; set; }
        [InverseProperty(nameof(OrderDetail.Order))]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
