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
        public string ConsignmentId { get; set; }
        [Display(Name = "Giá vốn")]
        public int CostPrice { get; set; }
        [Display(Name = "Tên nhà sản xuất")]
        public string Manufacturer { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Ngày sản xuất")]
        public DateTime ManufacturingDate { get; set; }

        [Display(Name = "Hạn sử dụng")]
        public DateTime ExpiresDate { get; set; }

        public ItemModels ItemModels { get; set; }
    }
}
