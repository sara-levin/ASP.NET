using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NewStart.Models
{
    public class Review
    {
        public int ID { get; set; }
        [Required, Column(TypeName = "nvarchar(255)")]
        public string Title { get; set; }
        [Required, Column(TypeName = "varchar(500)")]
        public string Content { get; set; }
 

        [Required]
        public string UserID { get; set; }
        public IdentityUser User { get; set; }
    }
}
