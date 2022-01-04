using System.ComponentModel.DataAnnotations;

namespace f7.Areas.Cart.Models
{
    public class CartViewModel
    {
        public string ItemsId { get; set; }
        [Display(Name = "Tên sản phẩm")]
        public string ItemName { get; set; }
        [Display(Name = "Số lượng")]
        public int Quantity { get; set; }
        [Display(Name = "Đơn giá")]
        public int SellingPrice { get; set; }
        [Display(Name = "Đơn vị tính")]
        public string Unit { get; set; }
        [Display(Name = "Thành tiền")]
        public int Amount { get; set; }
    }
}
