namespace f7.Models
{
    public class OrderState
    {
        public static string Waiting { get; } = "Chờ duyệt";
        public static string Accepted { get; } = "Đã duyệt";
        public static string Transporting { get; } = "Đang chuyển hàng";
        public static string Completed { get; } = "Hoàn thành";
        public static string Refunded { get; } = "Trả hàng";
        public static string Canceled { get; } = "Hủy";
    }

}
