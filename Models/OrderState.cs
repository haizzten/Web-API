namespace f7.Models
{
    public class OrderState
    {
        public static string Waiting { get; } = "Chưa thanh toán";
        public static string Paid { get; } = "Đã thanh toán";
        public static string Refunded { get; } = "Trả hàng";
        public static string Canceled { get; } = "Hủy";
    }

}
