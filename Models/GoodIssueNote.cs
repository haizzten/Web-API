using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace f7.Models{
    [Table("goodIssueNotes")]
    public class GoodIssueNoteModels{
        [Key]
        public string GoodIssueNoteId { get; set; }
        public DateTime DateTime { get; set; }
        public string StaffId { get; set; }
        public string State { get; set; }

        public StaffModels Staff { get; set; }
    }
}
