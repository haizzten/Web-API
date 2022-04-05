using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace f7.Models
{
    [Table("providers")]
    public class ProviderModels
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
