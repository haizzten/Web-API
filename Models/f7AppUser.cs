using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
#nullable enable
namespace f7.Models
{
    public class f7AppUser : IdentityUser
    {

        public string? DisplayName { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(400)]
        public string? HomeAdress { get; set; }

        // [Required]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
    }
}
