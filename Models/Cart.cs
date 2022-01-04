using System.ComponentModel.DataAnnotations;

namespace f7.Models
{
    public class CartModels
    {
        public string ItemId { get; set; }
        public int Quantity { get; set; }
        public ItemModels Item { get; set; }
    }
}
