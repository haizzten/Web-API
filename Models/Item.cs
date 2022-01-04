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

        [InverseProperty(nameof(OrderDetailModels.Item))]
        public virtual ICollection<OrderDetailModels> OrderDetails { get; set; }
    }
}
