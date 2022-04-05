using System;
using System.ComponentModel.DataAnnotations;

namespace f7.Models
{
    public class PurchaseOrderModels
    {
        [Key]
        public string PurchaseOrderId { get; set; }
        public string ProviderId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string StaffId { get; set; }
        public StaffModels Staff { get; set; }
        public ProviderModels Provider { get; set; }
    }
}
