using System.Collections.Generic;
using f7.Models;
namespace f7.Areas.Item
{
    public class ItemIndexViewModel
    {
        public IEnumerable<f7.Models.ItemModels> itemModels { get; set; }
        public f7.Models.ItemModels itemModel { get; set; } = new f7.Models.ItemModels();
        public string keyWord { get; set; }
        public List<string> itemsUnit { get; set; }
    }
}
