using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace f7.Models
{
    [Table("batches")]
    public class BatchModels
    {
        public string ID { get; set; }

        [Required]
        [Display(Name = "Mã lô hàng")]
        public string BatchId { get; set; }

        [Required]
        [Display(Name = "Mã S.P")]
        public string ItemId { get; set; }

        [Required]
        [Display(Name = "Đơn giá")]
        public int Price { get; set; }

        [Required]
        [Display(Name = "Nhập")]
        public int Quantity { get; set; }

        [Display(Name = "Thành tiền")]
        public int Amount { get; set; }

        [Display(Name = "Tồn")]
        public int Remain { get; set; }

        [Display(Name = "Tình trạng")]
        public string State { get; set; }

        [Display(Name = "Ngày nhập")]
        [DataType(DataType.DateTime)]
        public DateTime? ReceivingDate { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Ngày sản xuất")]
        public DateTime? ManufacturingDate { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Hạn sử dụng")]
        // [Column("ExpiresDate")]
        public DateTime? ExpireDate { get; set; }


        [Display(Name = "Mã phiếu nhập")]
        public string WarehouseReceiptId { get; set; }

        [ForeignKey("Provider")]
        public string GoodIssueId { get; set; }
        public string ProviderId { get; set; }

        public ProviderModels Provider { get; set; }
        public GoodIssueNoteModels GoodIssueNote { get; set; }
        public ItemModels Item { get; set; }
        public WarehouseReceiptModels WarehouseReceipt { get; set; }

    }
}
