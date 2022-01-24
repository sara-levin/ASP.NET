using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NewStart.Models
{
    public class MythologyUserReview
    {
        public int ID { get; set; }
        [Required, Column(TypeName = "nvarchar(255)")]
        public string Review { get; set; }
        [Required]
        public string UserID { get; set; }
        public IdentityUser User { get; set; }
        [Required]
        public int MythologyID { get; set; }
        public Mythology Mythology { get; set; }
    }
}
