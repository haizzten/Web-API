using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace f7.Models
{
    public class StockCardModels
    {
        public string ID { get; set; }
        [Display(Name = "Thời gian")]
        public DateTime DateTime { get; set; }
        [Display(Name = "Mã sản phẩm")]
        public string ItemId { get; set; }
        [Display(Name = "Nhập")]
        public int? In { get; set; }
        [Display(Name = "Xuất")]
        public int? Out { get; set; }
        [Display(Name = "Tồn")]
        public int? Balance { get; set; }

        [Display(Name = "Mã phiếu xuất")]
        public string BatchId { get; set; }

        public BatchModels Batch { get; set; }
        public virtual ItemModels Item { get; set; }
    }
}
