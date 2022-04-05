using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace f7.Models
{
    public class WarehouseReceiptModels
    {
        [Key]
        [Display(Name = "Mã phiếu nhập")]
        public string WarehouseReceiptId { get; set; }
        [Display(Name = "Thời gian lập phiếu")]
        public DateTime? DateTime { get; set; }
        [Display(Name = "Người giao")]
        public string DelivererName { get; set; }
        [Display(Name = "Mã đơn đặt hàng")]
        public string PurchaseOrderId { get; set; }
        [Display(Name = "Tên kho")]
        public string WarehouseName { get; set; }
        [Display(Name = "Tổng tiền")]
        public int TotalAmount { get; set; }

        public string WarehouseId { get; set; }

        public PurchaseOrderModels PurchaseOrder { get; set; }
        // public WarehouseModels Warehouse { get; set; }
    }
}
