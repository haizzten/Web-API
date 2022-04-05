namespace f7.Models
{
    public class BatchState
    {
        public static string InStock { get; } = "Trong kho";
        public static string InSelling { get; } = "Đang bán";
        public static string Sold { get; } = "Bán hết";
    }
}
