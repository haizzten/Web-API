using System;
using System.ComponentModel.DataAnnotations;

namespace f7.Models
{
    public class PurchaseOrderDetailModels
    {
        [Key]
        public string ID { get; set; }
        public string PurchaseOrderId { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }

        public PurchaseOrderModels PurchaseOrder { get; set; }
        public ItemModels Item { get; set; }
    }
}
