using System.ComponentModel.DataAnnotations;

namespace f7.Areas.Order
{
    public class OrderIndexViewModel
    {
        [Display(Name = "Mã đơn hàng")]
        public string OrderId { get; set; }
        [Display(Name = "Ngày tạo")]
        public string CreatedDate { get; set; }
        [Display(Name = "Mã khách hàng")]
        public string CustomerId { get; set; }
        [Display(Name = "Tên khách hàng")]
        public string CustomerName { get; set; }
        [Display(Name = "P.thức t.toán")]
        public string PaymentMethod { get; set; }
        [Display(Name = "Mã nhân viên")]
        public string StaffId { get; set; }
        [Display(Name = "Tình trạng")]
        public string State { get; set; }

    }
}
