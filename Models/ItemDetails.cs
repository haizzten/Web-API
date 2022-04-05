using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace f7.Models
{
    [Table("ItemDetail")]
    public class ItemDetailModels
    {
        public string ItemId { get; set; }

        [Display(Name = "Mã lô hàng")]
        public string BatchId { get; set; }

        [Display(Name = "Giá vốn")]
        public int OriginalPrice { get; set; }

        [Display(Name = "Mã nhà SX")]
        public string ProviderId { get; set; }

        [Display(Name = "Ngày nhập")]
        [DataType(DataType.DateTime)]
        public DateTime ReceivingDate { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Ngày sản xuất")]
        public DateTime ManufacturingDate { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Hạn sử dụng")]
        public DateTime ExpiresDate { get; set; }

        [Display(Name = "Tồn kho")]
        public int InStock { get; set; }

        [Display(Name ="Tình trạng")]
        public string State { get; set; }


        public ItemModels ItemModels { get; set; }
    }
}
