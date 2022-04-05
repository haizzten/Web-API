using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace f7.Models
{
    [Table("items")]
    public partial class ItemModels
    {
        public ItemModels()
        {
            OrderDetails = new HashSet<OrderDetailModels>();
        }

        [Key]
        [Display(Name = "Mã sản phẩm")]
        public string ItemId { get; set; }

        [Display(Name = "Tên sản phẩm")]
        public string ItemName { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Display(Name = "Đơn vị tính")]
        public string Unit { get; set; }

        [Display(Name = "Giá bán")]
        public int SellingPrice { get; set; }

        [Display(Name = "Liên kết ảnh")]
        public string Image { get; set; }

        [Display(Name = "Trong kho")]
        public int InStock { get; set; }

        [Display(Name = "Đang bán")]
        public int InSelling { get; set; }

        [Display(Name = "Nhắc nhở HSD trước")]
        public int NotifyBeforeDays { get; set; }
        [Display(Name = "Trạng thái")]
        public string State { get; set; }

        public ICollection<BatchModels> Batches { get; set; }

        [InverseProperty(nameof(OrderDetailModels.Item))]
        public virtual ICollection<OrderDetailModels> OrderDetails { get; set; }
    }
}
