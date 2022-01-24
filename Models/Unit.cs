using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NewStart.Models
{
    public class Unit
    {
        public int ID { get; set; }
        [Required, Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; }
        [Required, Column(TypeName = "nvarchar(255)")]
        public string Class { get; set; }
        [Required]
        public int Hitpoints { get; set; }
        [Required]
        public int Attack { get; set; }
        [Required]
        public int Cost { get; set; }
        [Required]
        public int MythologyID { get; set; }
        public Mythology Mythology { get; set; }
    }
}
