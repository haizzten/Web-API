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
    public partial class OrderModels
    {
        public OrderModels()
        {
            OrderDetails = new HashSet<OrderDetailModels>();
        }

        [Key]
        public string OrderId { get; set; }
        [Display(Name = "Ngày tạo")]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Mã khách hàng")]
        public string CustomerId { get; set; }
        [Display(Name = "P.thức t.toán")]
        public string StaffId { get; set; }
        [Display(Name = "Tổng tiền")]
        public int TotalAmount { get; set; }

        [Display(Name = "Tình trạng")]
        public string State { get; set; }

        [Display(Name = "Thếu GTGT")]
        public int VAT { get; set; }
        public string PaymentMethod { get; set; }



        public CustomerModels Customer { get; set; }
        [Display(Name = "Mã nhân viên")]

        [ForeignKey(nameof(StaffId))]
        [InverseProperty("Orders")]
        public virtual StaffModels Staff { get; set; }
        [InverseProperty(nameof(OrderDetailModels.Order))]
        public virtual ICollection<OrderDetailModels> OrderDetails { get; set; }
    }
}
