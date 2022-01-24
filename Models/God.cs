using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NewStart.Models
{
    public class God
    {
        public int ID { get; set; }
        [Required, Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; }
        public string Trivia { get; set; }
        [Required, Column(TypeName = "nvarchar(255)")]
        public string Age { get; set; }
        [Required, Column(TypeName = "nvarchar(255)")]
        public string Focus { get; set; }
        [Required]
        public int MythologyID { get; set; }
        public Mythology Mythology { get; set; }
    }
}
