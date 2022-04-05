using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace f7.Models.Models.Areas.Order
{
    public class DetailViewModel
    {
        public List<ItemViewModel> Items { get; set; } = new List<ItemViewModel>();
        [Display(Name = "Tổng")]
        public int TotalPrice { get; set; }
    }
    public class ItemViewModel
    {
        [Display(Name = "Tên sản phẩm")]
        public string ItemName { get; set; }
        [Display(Name = "Số lượng")]
        public int Quantity { get; set; }
        [Display(Name = "Đơn vị tính")]
        public string Unit { get; set; }
        [Display(Name = "Đơn giá")]
        public int Price { get; set; }
        [Display(Name = "Thành tiền ")]
        public int Amount { get; set; }
    }
}
